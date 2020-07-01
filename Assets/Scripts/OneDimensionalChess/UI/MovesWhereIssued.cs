using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OneDimensionalChess.UI
{
    /// <summary>
    /// Moves a <see cref="PieceView"/> wherever<see cref="IssuesMoves"/> says so.
    /// </summary>
    public class MovesWhereIssued : UIBehaviour
    {
        [SerializeField] private PieceView m_PieceView;
        [SerializeField] private IssuesMoves m_IssuesMoves;

        protected override void Start()
        {
            base.Start();
            m_IssuesMoves.commands
                .Subscribe(destination =>
                {
                    var piece = m_PieceView.piece.Value;
                    piece.position = destination;
                    m_PieceView.piece.SetValueAndForceNotify(piece);
                })
                .AddTo(this);
        }
    }
}