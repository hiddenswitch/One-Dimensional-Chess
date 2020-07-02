using System;
using System.Collections.Generic;
using OneDimensionalChess.Model;
using UniRx;

namespace OneDimensionalChess.UI
{
    [Serializable]
    public class PieceReactiveProperty : ReactiveProperty<Piece>
    {
        
        protected override IEqualityComparer<Piece> EqualityComparer => EqualityComparer<Piece>.Default;
    }
}