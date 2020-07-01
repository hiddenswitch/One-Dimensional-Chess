using OneDimensionalChess.Model;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneDimensionalChess.UI
{
    /// <summary>
    /// Renders the <see cref="OneDimensionalChess.Model.Piece"/> we are given.
    /// </summary>
    public class PieceView : UIBehaviour
    {
        [Header("Style")] [SerializeField] private OneDimensionalChessStyle m_OneDimensionalChessStyle;
        [Header("Data/Model")] [SerializeField]
        private PieceReactiveProperty m_Piece;
        [Header("Other Views")] [SerializeField]
        private Image m_Image;
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private CanvasGroup m_CanvasGroup;
        public ReactiveProperty<Piece> piece => m_Piece;


        protected override void Start()
        {
            base.Start();

            piece
                .Subscribe(currentPiece => { Render(currentPiece); })
                .AddTo(this);
        }

        private void Render(Piece currentPiece)
        {
            if (m_Image != null && m_OneDimensionalChessStyle != null)
            {
                m_Image.sprite = m_OneDimensionalChessStyle.pieceTypeColorSprites[new PieceTypeColor()
                {
                    isBlack = currentPiece.isBlack,
                    pieceType = currentPiece.type
                }]; /*correct sprite for this piece*/
            }

            if (m_RectTransform != null)
            {
                m_RectTransform.anchoredPosition = new Vector2(0, GameContext.pieceSize * currentPiece.position);
            }

            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = currentPiece.taken ? 0f : 1f;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Render(piece.Value);
        }
#endif
    }
}