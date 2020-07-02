using System;
using OneDimensionalChess.Model;
using UniRx;
using UniRx.Diagnostics;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneDimensionalChess.UI
{
    /// <summary>
    /// Issues a move when the <see cref="Piece" /> this is attached to is dragged and dropped.
    /// </summary>
    public class IssuesMoves : UIBehaviour
    {
        private ISubject<int> m_Commands = new Subject<int>();
        public IObservable<int> commands => m_Commands;

        protected override void Start()
        {
            base.Start();
            var pieceView = GetComponent<PieceView>();

            var startPosition = Vector2.zero;
            var rectTransform = transform as RectTransform;
            var graphic = GetComponent<Graphic>();
            var scaleFactor = graphic.canvas.scaleFactor;
            var startPiecePosition = 0;

            this.OnBeginDragAsObservable()
                .Subscribe(_ =>
                {
                    startPosition = rectTransform.anchoredPosition;
                    startPiecePosition = pieceView.piece.Value.position;
                })
                .AddTo(this);
            this.OnDragAsObservable()
                .Subscribe(pointerEvent =>
                {
                    var newPosition = rectTransform.anchoredPosition + pointerEvent.delta * (1f / scaleFactor);
                    rectTransform.anchoredPosition = newPosition;
                })
                .AddTo(this);

            this.OnEndDragAsObservable()
                .Select(_ =>
                {
                    var droppedPosition = rectTransform.anchoredPosition;
                    // distance along the board
                    var canvasDistance = Vector2.Dot(droppedPosition - startPosition, GameContext.boardAxis.normalized);
                    var destinationPosition =
                        startPiecePosition + (int) (canvasDistance + 0.5f) / GameContext.pieceSize;
                    return destinationPosition;
                })
                .Subscribe(m_Commands)
                .AddTo(this);
        }
    }
}