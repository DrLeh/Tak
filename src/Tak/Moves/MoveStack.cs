using System;
using System.Collections.Generic;
using System.Linq;

namespace Tak
{
    public class MoveStack : Move
    {
        public class MoveDrop
        {
            public int NumToDrop { get; set; }
            public Square Square { get; set; }
        }

        public StoneStack Stack { get; }
        public Board Board { get; }
        public Square From { get; }
        public Square Destination { get; }
        public int[] ToDrop { get; }
        public List<MoveDrop> MoveDrops { get; set; }

        public override bool RequiresNewPiece => false;

        public MoveStack(StoneStack stack, Board board, List<MoveDrop> moveDrops)
        {
            Stack = stack;
            Board = board;
            //From = from;
            //Destination = destination;
            MoveDrops = moveDrops;
        }

        public override bool IsValid_Impl()
        {
            //if (ToDrop.Sum() > Stack.Count())
                //return false;// trying to drop too many
            if (!From.IsInStraightLineWith(Destination))
                return false;

            var stackCopy = Stack.Copy();

            foreach (var item in stackCopy)
            {
                // need to iterate from the bottom
            }

            return false;
        }

        public override void Perform(Board b)
        {
            throw new NotImplementedException();
        }
    }
}
