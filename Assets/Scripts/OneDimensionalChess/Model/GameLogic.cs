using System;
using System.Linq;

namespace OneDimensionalChess.Model
{
    public static class GameLogic
    {
        private struct GameAction : IGameAction
        {
            public Piece piece { get; set; }
            public int destination { get; set; }

            public int forTurn { get; set; }
            public ActionType actionType { get; set; }
        }

        /// <summary>
        /// Get valid actions for the given game state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IGameAction[] GetValidActions(GameState state)
        {
            var isBlackTurn = state.isBlackTurn;

            return state.pieces
                .Where(p => p.isBlack == isBlackTurn && !p.taken)
                .SelectMany(p =>
                {
                    return Enumerable.Range(0, state.size)
                        .Where(i => i != p.position)
                        .Select(destination => new GameAction()
                        {
                            piece = p,
                            destination = destination,
                            forTurn = state.turn,
                            actionType = ActionType.MOVE
                        });
                })
                .Select(ga => (IGameAction) ga)
                /* TODO: Don't omit concede
                .Append(new GameAction()
                {
                    actionType = ActionType.CONCEDE,
                    forTurn = state.turn
                })*/
                .ToArray();
        }

        /// <summary>
        /// Perform an action
        /// </summary>
        /// <param name="state"></param>
        /// <param name="gameAction">Created from <see cref="GetValidActions"/></param>
        /// <returns></returns>
        public static GameState PerformGameAction(GameState state, IGameAction gameAction)
        {
            // TODO: Handle concede
            if (gameAction.forTurn != state.turn)
            {
                return state;
            }

            // First instead of FirstOrDefault throws an exception if an item with the specified predicate
            // cannot be found
            var piece = gameAction.piece;
            var indexOfPiece = state.pieces
                .Select((p, i) => (p, i))
                .Where(p => piece.id == p.p.id)
                .Select(tuple => tuple.i).First();

            // check if a piece exists in the destination
            var (pieceAtDestination, indexOfDestinationPiece) =
                state.pieces.Select((p, i) => (piece: (Piece?) p, i))
                    .FirstOrDefault(p => p.piece.Value.position == gameAction.destination);

            piece.position = gameAction.destination;
            state.pieces = state.pieces.ToArray();
            state.pieces[indexOfPiece] = piece;

            if (pieceAtDestination != null)
            {
                var takenPiece = pieceAtDestination.Value;
                takenPiece.taken = true;
                state.pieces[indexOfDestinationPiece] = takenPiece;
            }

            state.turn++;
            return state;
        }
    }
}