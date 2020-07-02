using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OneDimensionalChess.UI
{
    public class Player : UIBehaviour
    {
        [SerializeField] public IssuesMoves[] m_Pieces;
        [SerializeField] public bool m_IsBlack;

        public bool isBlack => m_IsBlack;

        protected override void Start()
        {
            base.Start();
            
            m_Pieces.ToObservable()
                .SelectMany(moveIssuer =>
                {
                    var pieceView = moveIssuer.GetComponent<PieceView>();
                    return moveIssuer.commands.Select(destination => (pieceView.piece.Value, destination));
                })
                .Subscribe(tuple =>
                {
                    var (piece, destination) = tuple;

                    GameController.instance.gameContext.Move(piece, destination, m_IsBlack);
                });
        }
    }
}