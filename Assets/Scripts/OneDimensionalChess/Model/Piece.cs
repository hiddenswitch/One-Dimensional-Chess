using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDimensionalChess.Model
{
    [Serializable]
    public struct Piece
    {
        [SerializeField] private int m_Id;
        [SerializeField] private PieceType m_Type;
        [SerializeField] private bool m_IsBlack;
        [SerializeField] private bool m_Taken;
        [SerializeField] private int m_Position;

        public Piece(int id) : this()
        {
            m_Id = id;
        }

        public PieceType type
        {
            get => m_Type;
            set => m_Type = value;
        }
        public bool isBlack
        {
            get => m_IsBlack;
            set => m_IsBlack = value;
        }
        public int position
        {
            get => m_Position;
            set => m_Position = value;
        }
        public bool taken
        {
            get => m_Taken;
            set => m_Taken = value;
        }

        public int id => m_Id;

        private sealed class IdEqualityComparer : IEqualityComparer<Piece>
        {
            public bool Equals(Piece x, Piece y)
            {
                return x.m_Id == y.m_Id;
            }

            public int GetHashCode(Piece obj)
            {
                return obj.m_Id;
            }
        }

        public static IEqualityComparer<Piece> idComparer { get; } = new IdEqualityComparer();
    }
}