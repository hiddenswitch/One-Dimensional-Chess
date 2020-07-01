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
        }
    }
}