using System;
using OneDimensionalChess.Model;
using UnityEngine;

namespace OneDimensionalChess.UI
{
    [Serializable]
    public struct PieceTypeColor
    {
        [SerializeField] private PieceType m_PieceType;
        [SerializeField] private bool m_IsBlack;

        public PieceType pieceType
        {
            get => m_PieceType;
            set => m_PieceType = value;
        }
        public bool isBlack
        {
            get => m_IsBlack;
            set => m_IsBlack = value;
        }
    }
}