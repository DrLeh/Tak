using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tak.MoveStack;

namespace Tak
{

    public static class MoveParser
    {
        public const char Up = '+';
        public const char Right = '>';
        public const char Down = '-';
        public const char Left = '<';
        public static readonly char[] Actions = new char[] { Up, Right, Down, Left };

        //https://www.reddit.com/r/Tak/wiki/portable_tak_notation
        public static Move ParseMove(string input, Board board, Color color)
        {
            bool isPlaceNew = true;
            foreach (var a in Actions)
                if (input.Contains(a)) // no special chars, just a placement
                    isPlaceNew = false;

            if (isPlaceNew)
                return PlaceNew(input, board, color);

            //if (input.Split(Actions).Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 1)
            //    return MoveSingle(input, board, color);

            return MoveStack(input, board, color);
        }

        public static NewPieceMove PlaceNew(string input, Board board, Color color)
        {
            var file = Board.Files.IndexOf(input[0].ToString().ToUpper()[0]);
            var rank = int.Parse(input[1].ToString()) - 1;
            Piece piece = new Road();
            if (input.Length > 2)
            {
                var typeShort = input[2];

                switch (typeShort)
                {
                    case 's':
                    case 'w':
                        piece = new StandingStone(); break;
                    case 'c':
                        piece = new Capstone(); break;
                }
            }
            piece.X = file;
            piece.Y = rank;
            piece.Color = color;

            //todo: switch based on further input
            var square = board.GetSquare(piece.X, piece.Y);
            var move = new NewPieceMove(piece, square);

            return move;
        }

        public static MoveStack MoveStack(string input, Board board, Color color)
        {
            int file = 0;
            int rank = 0;

            var counter = 0;

            int howManyToTake = 1;
            var first = input[counter];
            if (char.IsNumber(input[counter]))
            {
                howManyToTake = int.Parse(input[counter].ToString());
                counter++;

                if (howManyToTake > board.Dimension)
                    throw new InvalidOperationException($"Cannot take more than Board dimension # of pieces ({board.Dimension})");
            }

            //file@
            file = Board.Files.IndexOf(input[counter].ToString().ToUpper()[0]); //get the index from the letter of the file
            counter++;

            //rank
            rank = int.Parse(input[counter].ToString()) - 1;
            counter++;

            //driection
            var direction = input[counter];
            counter++;

            //drop values
            var remainder = input.Substring(counter);

            var dropValues = remainder.Trim().Select(x => int.Parse(x.ToString().Trim())).ToList();
            if (!dropValues.Any()) // if no number specified, assume one
                dropValues.Add(1);

            if (howManyToTake != dropValues.Sum())
                throw new InvalidOperationException("Not accounting for each stone taken");

            //now build the move sets based on this.
            var moveDrops = new List<MoveDrop>();

            var sourceSquare = board.GetSquare(file, rank);

            foreach (var drop in dropValues)
                moveDrops.Add(MoveInDirection(ref file, ref rank, direction, drop));

            var (bottom, top) = StoneStack.Split(sourceSquare.Pieces, howManyToTake);
            var ret = new MoveStack(top, board, moveDrops);
            return ret;
        }

        public static MoveDrop MoveInDirection(ref int file, ref int rank, char direction, int drop)
        {
            switch (direction)
            {
                case Up: return new MoveDrop { NumToDrop = drop, Square = new Square { X = file, Y = ++rank } };
                case Right: return new MoveDrop { NumToDrop = drop, Square = new Square { X = ++file, Y = rank } };
                case Down: return new MoveDrop { NumToDrop = drop, Square = new Square { X = file, Y = --rank } };
                case Left: return new MoveDrop { NumToDrop = drop, Square = new Square { X = --file, Y = rank } };
            }
            throw new InvalidOperationException("unrecognized driection");
        }
    }
}
