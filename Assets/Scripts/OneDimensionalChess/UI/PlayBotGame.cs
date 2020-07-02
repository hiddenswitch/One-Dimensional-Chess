using UnityEngine;
using UnityEngine.EventSystems;

namespace OneDimensionalChess.UI
{
    public class PlayBotGame : UIBehaviour
    {
        [SerializeField] private Player m_Player;
        protected override void Start()
        {
            base.Start();

            GameController.instance.gameContext.PlayAgainstBot(!m_Player.isBlack);
        }
    }
}