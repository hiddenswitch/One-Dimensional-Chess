using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OneDimensionalChess.UI
{
    /// <summary>
    /// Connect the piece view to a specific piece in the game state on a game controller
    /// </summary>
    /// This is also known as a "ModelView" in this pattern
    public class BindPieceViewToGameController : UIBehaviour
    {
        [SerializeField] private int m_Id;
        [SerializeField] private PieceView m_PieceView;

        protected override void Start()
        {
            base.Start();

            // If we didn't set a piece view just return
            if (m_PieceView == null)
            {
                return;
            }

            GameController.instance.gameContext.gameState
                .SelectMany(gameState => gameState.pieces)
                .Where(piece => piece.id == m_Id)
                .Subscribe(piece => { m_PieceView.piece.Value = piece; })
                .AddTo(this);
        }
    }
}