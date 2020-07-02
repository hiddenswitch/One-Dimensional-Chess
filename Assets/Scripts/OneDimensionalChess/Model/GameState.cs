using System;
using System.Linq;
using UnityEngine;

namespace OneDimensionalChess.Model
{
    [Serializable]
    public struct GameState
    {
        /// <summary>
        /// Size of the board in tiles
        /// </summary>
        [SerializeField] private int m_Size;
        [SerializeField] private Piece[] m_Pieces;
        [SerializeField] private int m_Turn;
        [SerializeField] private bool m_Simulated;

        public int size
        {
            get => m_Size;
            set => m_Size = value;
        }
        public Piece[] pieces
        {
            get => m_Pieces;
            set => m_Pieces = value;
        }

        public int turn
        {
            get => m_Turn;
            set => m_Turn = value;
        }

        private static readonly GameState m_Sacksons = new GameState()
        {
            size = 12,
            pieces = new[]
            {
                new Piece(0) {position = 0, type = PieceType.ROOK},
                new Piece(1) {position = 1, type = PieceType.KNIGHT},
                new Piece(2) {position = 2, type = PieceType.KING},
                new Piece(3) {position = 3, type = PieceType.KNIGHT},
                new Piece(4) {position = 4, type = PieceType.ROOK},
                new Piece(5) {isBlack = true, position = 7, type = PieceType.ROOK},
                new Piece(6) {isBlack = true, position = 8, type = PieceType.KNIGHT},
                new Piece(7) {isBlack = true, position = 9, type = PieceType.KING},
                new Piece(8) {isBlack = true, position = 10, type = PieceType.KNIGHT},
                new Piece(9) {isBlack = true, position = 11, type = PieceType.ROOK},
            }
        };

        public bool simulated
        {
            get => m_Simulated;
            set => m_Simulated = value;
        }

        public static GameState sacksons => m_Sacksons;

        public bool isBlackTurn => turn % 2 == 1;

        public override string ToString()
        {
            return $"{nameof(pieces)}: {pieces}, {nameof(turn)}: {turn}, {nameof(isBlackTurn)}: {isBlackTurn}";
        }
    }
}