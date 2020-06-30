using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OneDimensionalChess.UI
{
    public class DebugLogGameState : UIBehaviour
    {
        protected override void Start()
        {
            base.Start();

            GameController.instance.gameContext.gameState
                .Debug(
                    $"{nameof(GameController)}.{nameof(GameController.instance)}.{nameof(GameController.gameContext)}.{nameof(GameContext.gameState)}")
                .Subscribe()
                .AddTo(this);
        }
    }
}