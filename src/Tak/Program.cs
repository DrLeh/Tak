using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tak
{
    class Program
    {
        static void Main(string[] args)
        {
            var light = "Yagami"; // light yagami
            var dark = "Zeratul"; // dark templar, z > y
            var board = new Board(6, light, dark);


            var counter = 1;
            var player = board.LightPlayer;
            bool quit = false;
            while (true)
            {
                
                Console.Write(board.Display());

                Console.WriteLine();
                var colorToPlace = player.Color;
                if (counter == 1)
                {
                    if (player.Color == Color.Light)
                        colorToPlace = Color.Dark;
                    else
                        colorToPlace = Color.Light;
                }
                retry:
                Console.WriteLine($"${player.Name}'s turn to place a {colorToPlace} tile. Enter a string to place a piece. e.g. A1 for a road on A1. Add S or C for wall or cap. q to quit");
                var input = Console.ReadLine().Trim().ToLower();

                if (input == "q")
                    break;

                //parse move

                var (piece, move) = ParseMove(board, input, colorToPlace);

                if (move.RequiresNewPiece)
                {
                    //check if you have enough
                }

                if(!move.IsValid())
                {
                    Console.WriteLine("Invalid move");
                    goto retry;
                }

                move.Perform(board);
            }


            Console.WriteLine();
        }

        public static (Piece piece, Move move) ParseMove(Board board, string input, Color color)
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
                    case 'c': piece = new Capstone(); break;
                }
            }
            piece.X = file;
            piece.Y = rank;
            piece.Color = color;

            //todo: switch based on further input
            var square = board.GetSquare(piece.X, piece.Y);
            var move = new NewPieceMove(piece, square);

            return (piece, move);
        }
    }

    public enum ColorEnum
    {
        Light,
        Dark
    }

    public class Color
    {
        public ColorEnum Enum { get; }
        private Color(ColorEnum ce)
        {
            Enum = ce;
        }

        public readonly static Color Light = new Color(ColorEnum.Light);
        public readonly static Color Dark = new Color(ColorEnum.Dark);

        public override string ToString() => this == Light ? "L" : "D";
    }

    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }

    public class Board
    {
        public Board(int dimension, string light, string dark)
        {
            Dimension = dimension;
            LightPlayer = new Player { Color = Color.Light, Name = light };
            DarkPlayer = new Player { Color = Color.Dark, Name = dark };

            Squares = new Square[Dimension, Dimension];

            InitSquares();
        }

        public IEnumerable<(int x, int y)> EnumeratePositions()
        {
            for (int x = 0; x < Dimension; x++)
                for (int y = 0; y < Dimension; y++)
                    yield return (x, y);
        }

        public IEnumerable<IEnumerable<(int x, int y)>> EnumerateRanks()
        {
            IEnumerable<(int x, int y)> Rank(int x)
            {
                for (int y = 0; y < Dimension; y++)
                    yield return (x, y);
            }

            for (int x = 0; x < Dimension; x++)
                yield return Rank(x);
        }

        private void InitSquares()
        {
            foreach (var (x, y) in EnumeratePositions())
                Squares[x, y] = Square.Empty(x, y);
        }

        public int Dimension { get; }

        public Player LightPlayer { get; }
        public Player DarkPlayer { get; }

        public Square[,] Squares { get; set; }

        public const string Files = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Square GetSquare(int x, int y) => Squares[x, y];

        public void AddPiece(Piece p)
        {
            Squares[p.X, p.Y].Pieces.Push(p);
        }

        public void Move(Move m)
        {

        }

        public string Display()
        {
            var sb = new StringBuilder();
            sb.Append("    ");
            for (int i = 0; i < Dimension; i++)
                sb.Append(i + 1).Append(" ");
            sb.AppendLine().Append("    ");
            for (int i = 0; i < Dimension; i++)
                sb.Append("--");

            foreach (var rank in EnumerateRanks())
            {
                // label
                sb.AppendLine().Append(Files[rank.First().x]).Append(" | ");

                foreach (var (x, y) in rank)
                    sb.Append(Squares[x, y].ToString()).Append(" ");

            }

            return sb.ToString();
        }
    }

    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
    }

    public class Square : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public StoneStack Pieces = new StoneStack();

        public static Square Empty(int x, int y) => new Square { X = x, Y = y };

        public bool IsEmpty => !Pieces.Any();

        public void Push(Color color, PieceType type)
        {
            Piece piece;
            if (type == PieceType.Road)
                piece = new Road { Color = color };
            else if (type == PieceType.Standing)
                piece = new Road { Color = color };
            else if (type == PieceType.Standing)
                piece = new Road { Color = color };
        }

        public override string ToString()
        {
            return Pieces.ToString();
        }

        public bool IsInStraightLineWith(Square other)
        {
            return X == other.X || Y == other.Y;
        }

        public bool IsAdjacentTo(Square other)
        {
            if (X == other.X && Y == other.Y || !IsInStraightLineWith(other))
                return false; //same square

            //in line in X axis
            if (X == other.X && (Y - 1 == other.Y || Y + 1 == other.Y))
                return true;

            //in line in Y axis
            if (Y == other.Y && (X - 1 == other.X || X + 1 == other.X))
                return true;

            return false; //in line but not adjacent
        }
    }

    public enum PieceType
    {
        Road,
        Standing,
        Capstone
    }

    public abstract class Piece : IPosition
    {
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Road : Piece
    {
        public override string ToString() => $"{X}:{Y}:{Color}R";
    }

    public class StandingStone : Piece
    {
        public override string ToString() => $"{X}:{Y}:{Color}S";
    }

    public class Capstone : Road
    {
        public override string ToString() => $"{X}:{Y}:{Color}C";
    }

}
