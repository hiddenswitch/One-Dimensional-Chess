using System;
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

        public const int pieceSize = 40;
        public static readonly Vector2 boardAxis = Vector2.up;
    }
}