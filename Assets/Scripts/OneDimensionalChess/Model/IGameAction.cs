namespace OneDimensionalChess.Model
{
    public interface IGameAction
    {
        Piece piece { get; }
        int destination { get; }
        int forTurn { get; }
        ActionType actionType { get; }
    }
}