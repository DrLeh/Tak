using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tak.Test
{
    public class MoveParserTests
    {
        [Fact]
        public void NewPIeceMove_Test()
        {
            var board = new Board(5, "", "");
            var move = MoveParser.ParseMove("a1", board, Color.Light);

            move.Should().BeOfType<NewPieceMove>();
        }

        [Fact]
        public void MoveStackOfOne_Test()
        {
            var board = new Board(5, "", "");
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            var move = MoveParser.ParseMove("a1>", board, Color.Light) as MoveStack;

            move.Should().NotBeNull(); // already casted
            var pieceOnB1 = move.Stack.Dequeue();
            pieceOnB1.X.Should().Be(0);
            pieceOnB1.Y.Should().Be(0);

            var drop = move.MoveDrops.First();
            drop.Square.X.Should().Be(1);
            drop.Square.Y.Should().Be(0);
        }

        [Fact]
        public void MoveStackOfTwo_Test()
        {
            var board = new Board(5, "", "");
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            var move = MoveParser.ParseMove("2a1>11", board, Color.Light) as MoveStack;

            move.Should().NotBeNull(); // already casted
            move.Stack.First.Value.X.Should().Be(0);
            move.Stack.First.Value.Y.Should().Be(0);

            var drop = move.MoveDrops.First();
            drop.Square.X.Should().Be(1);
            drop.Square.Y.Should().Be(0);
            var pieceOnB1 = move.Stack.Dequeue();
            pieceOnB1.Color.Should().Be(Color.Light);

            drop = move.MoveDrops.Skip(1).First();
            drop.Square.X.Should().Be(2);
            drop.Square.Y.Should().Be(0);
            var pieceOnC1 = move.Stack.Dequeue();
            pieceOnC1.Color.Should().Be(Color.Dark);
        }

        [Fact]
        public void MoveStackOfTwo_Take1_Test()
        {
            var board = new Board(5, "", "");
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            var move = MoveParser.ParseMove("1a1>", board, Color.Light) as MoveStack;

            move.Should().NotBeNull(); // already casted
            move.Stack.First.Value.X.Should().Be(0);
            move.Stack.First.Value.Y.Should().Be(0);

            var drop = move.MoveDrops.First();
            drop.Square.X.Should().Be(1);
            drop.Square.Y.Should().Be(0);
            var pieceOnB1 = move.Stack.Dequeue();
            pieceOnB1.Color.Should().Be(Color.Dark); //take the dark, leave the light

            move.Stack.Should().BeEmpty();
        }

        [Fact]
        public void MoveStackOf10_Take4_Test()
        {
            var board = new Board(5, "", "");
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            var move = MoveParser.ParseMove("4a1>22", board, Color.Light) as MoveStack;

            move.Should().NotBeNull(); // already casted
            move.Stack.First.Value.X.Should().Be(0); //source square
            move.Stack.First.Value.Y.Should().Be(0);

            move.MoveDrops.Count().Should().Be(2);
            var drop = move.MoveDrops.First();
            drop.NumToDrop.Should().Be(2);
            drop.Square.X.Should().Be(1);
            drop.Square.Y.Should().Be(0);
            var pieceOnB1 = move.Stack.Dequeue();
            pieceOnB1.Color.Should().Be(Color.Light);
            var pieceOnB12 = move.Stack.Dequeue();
            pieceOnB12.Color.Should().Be(Color.Dark);

            drop = move.MoveDrops.Skip(1).First();
            drop.NumToDrop.Should().Be(2);
            drop.Square.X.Should().Be(2);
            drop.Square.Y.Should().Be(0);
            var pieceOnC1 = move.Stack.Dequeue();
            pieceOnC1.Color.Should().Be(Color.Light);
            var pieceOnC12 = move.Stack.Dequeue();
            pieceOnC12.Color.Should().Be(Color.Dark);

            move.Stack.Should().BeEmpty();
        }


        [Fact]
        public void MoveBiggerThanDimensionAllows()
        {
            var board = new Board(8, "", "");
            Assert.Throws<InvalidOperationException>(() => MoveParser.ParseMove("9a1>", board, Color.Light));
        }

        [Fact]
        public void SpecifyMoreDropsThanTaking()
        {
            var board = new Board(5, "", "");
            board.AddPiece(new Road { Color = Color.Light, X = 0, Y = 0 });
            board.AddPiece(new Road { Color = Color.Dark, X = 0, Y = 0 });
            Assert.Throws<InvalidOperationException>(() => MoveParser.ParseMove("2a1>21", board, Color.Light));
        }
    }
}
