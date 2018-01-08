namespace Tak
{
    public class NewPieceMove : Move
    {
        public Piece Piece { get; }
        public Square Square { get; }

        public override bool RequiresNewPiece => true;

        public NewPieceMove(Piece piece, Square square)
        {
            Piece = piece;
            Square = square;
        }

        public override bool IsValid_Impl()
        {
            return Square.IsEmpty;
        }

        public override void Perform(Board b)
        {
            b.AddPiece(Piece);
        }
    }
}
