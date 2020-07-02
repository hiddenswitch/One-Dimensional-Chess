using System;
using System.Linq;
using OneDimensionalChess.Model;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace OneDimensionalChess
{
    /// <summary>
    /// Runs a game of OneDimensionalChess
    /// </summary>
    public class GameContext
    {
        private ReactiveProperty<GameState> m_GameState = new ReactiveProperty<GameState>(GameState.sacksons);

        public IReadOnlyReactiveProperty<GameState> gameState => m_GameState;

        public IDisposable PlayGameRandomly(IObservable<Unit> takeNextMove)
        {
            var random = new Random();
            IDisposable disposable = null;
            disposable = takeNextMove.Subscribe(_ =>
            {
                var validActions = GameLogic.GetValidActions(gameState.Value);
                if (validActions.Length == 0)
                {
                    // Dispose, we're done, game over
                    disposable?.Dispose();
                    return;
                }

                var chosenAction = validActions[random.Next(validActions.Length)];
                m_GameState.Value = GameLogic.PerformGameAction(gameState.Value, chosenAction);
            });
            return disposable;
        }

        public IDisposable PlayAgainstBot(bool botIsBlack = true)
        {
            var random = new Random();
            IDisposable disposable = null;
            // Bot moves
            disposable = gameState.Where(gs => gs.isBlackTurn == botIsBlack)
                .Where(gs => !gs.simulated)
                .Delay(TimeSpan.FromSeconds(1f))
                .Subscribe(gs =>
                {
                    var validActions = GameLogic.GetValidActions(gameState.Value);
                    if (validActions.Length == 0)
                    {
                        // Dispose, we're done, game over
                        disposable?.Dispose();
                        return;
                    }

                    var chosenAction = validActions[random.Next(validActions.Length)];
                    m_GameState.SetValueAndForceNotify(GameLogic.PerformGameAction(gs, chosenAction));
                });

            // Start the game
            m_GameState.SetValueAndForceNotify(GameState.sacksons);

            return disposable;
        }

        public const int pieceSize = 40;
        public static readonly Vector2 boardAxis = Vector2.up;

        public void Move(Piece piece, int destination, bool isBlack)
        {
            // client-side simulation: immediately mutate the gamestate so that the piece has the new destination
            // i.e., assume it is correct
            var originalGameState = gameState.Value;
            var simulatedGameState = gameState.Value;
            simulatedGameState.simulated = true;
            simulatedGameState.pieces = simulatedGameState.pieces.ToArray();
            for (var i = 0; i < simulatedGameState.pieces.Length; i++)
            {
                if (simulatedGameState.pieces[i].id == piece.id)
                {
                    var updatedPiece = simulatedGameState.pieces[i];
                    updatedPiece.position = destination;
                    simulatedGameState.pieces[i] = updatedPiece;
                }
            }

            m_GameState.SetValueAndForceNotify(simulatedGameState);

            // This is the remote call, but it obviously happens instantaneously here
            var result = GameLogic
                .GetValidActions(originalGameState)
                .Where(validAction =>
                {
                    return validAction.actionType == ActionType.MOVE
                           && ((validAction.forTurn % 2 == 1) == isBlack)
                           && validAction.piece.id == piece.id
                           && validAction.destination == destination;
                }).Take(1).ToList();
            if (result.Count == 1)
            {
                m_GameState.SetValueAndForceNotify(GameLogic.PerformGameAction(originalGameState, result[0]));
            }
            else
            {
                // Undo!
                m_GameState.SetValueAndForceNotify(originalGameState);
            }
        }
    }
}