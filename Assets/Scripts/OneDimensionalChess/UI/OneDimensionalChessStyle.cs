using UnityEngine;

namespace OneDimensionalChess.UI
{
    [CreateAssetMenu(menuName = "Chess Style")]
    public class OneDimensionalChessStyle : ScriptableObject
    {
        [SerializeField]
        private PieceTypeColorSpriteDictionary m_PieceTypeColorSprites = new PieceTypeColorSpriteDictionary();

        public PieceTypeColorSpriteDictionary pieceTypeColorSprites => m_PieceTypeColorSprites;
    }
}