namespace Tak
{
    public abstract class Move
    {
        public bool IsValid()
        {
            //todo: add validation to make sure this person controls the square
            return IsValid_Impl();
        }

        public abstract bool IsValid_Impl();

        public abstract bool RequiresNewPiece { get; }

        public abstract void Perform(Board b);
    }
}
