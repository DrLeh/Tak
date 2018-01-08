using System;

namespace Tak
{
    public class MoveSingle : Move
    {
        public Square From { get; }
        public Square To { get; }

        public override bool RequiresNewPiece => false;

        public MoveSingle(Piece p, Square from, Square to)
        {
            From = from;
            To = to;
        }

        public override bool IsValid_Impl()
        {
            return From.IsAdjacentTo(To);
        }

        public override void Perform(Board b)
        {
            throw new NotImplementedException();
        }
    }
}
