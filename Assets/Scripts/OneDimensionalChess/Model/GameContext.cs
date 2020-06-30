using System;
using OneDimensionalChess.Model;
using UniRx;

namespace OneDimensionalChess
{
    public class GameContext
    {
        private Random m_Random = new Random();
        private ReactiveProperty<GameState> m_GameState = new ReactiveProperty<GameState>(GameState.sacksons);

        public IReadOnlyReactiveProperty<GameState> gameState => m_GameState;

        public IDisposable PlayGameRandomly(IObservable<Unit> takeNextMove)
        {
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

                var chosenAction = validActions[m_Random.Next(validActions.Length)];
                m_GameState.Value = GameLogic.PerformGameAction(gameState.Value, chosenAction);
            });
            return disposable;
        }
    }
}