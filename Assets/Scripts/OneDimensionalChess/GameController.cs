using System;
using UniRx;
using UnityEngine.EventSystems;

namespace OneDimensionalChess
{
    public class GameController : UIBehaviour
    {
        public static GameController instance { get; private set; }
        public GameContext gameContext { get; } = new GameContext();

        protected override void Awake()
        {
            base.Awake();
            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            // Instead of play button that starts the game and whatever the fuck, we're going
            // to play the game randomly stepping every second

            gameContext.PlayGameRandomly(Observable.Interval(TimeSpan.FromSeconds(1f)).AsUnitObservable())
                .AddTo(this);
        }
    }
}