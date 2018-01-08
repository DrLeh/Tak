using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Tak.Test
{
    public class StoneStackTest
    {
        public Piece Dark => new Road { Color = Color.Dark };
        public Piece Light => new Road { Color = Color.Light };

        [Fact]
        public void StoneStack_Push_Pop_Test()
        {
            var stack = new StoneStack();
            stack.Push(Dark);
            stack.Push(Light);

            stack.Pop().Color.Should().Be(Color.Light);
            stack.Pop().Color.Should().Be(Color.Dark);
        }

        [Fact]
        public void StoneStack_Push_Dequeue_Test()
        {
            var stack = new StoneStack();
            stack.Push(Dark);
            stack.Push(Light);

            stack.Dequeue().Color.Should().Be(Color.Dark);
            stack.Dequeue().Color.Should().Be(Color.Light);
        }

        [Fact]
        public void StoneStack_Enqueue_Dequeue_Test()
        {
            var stack = new StoneStack();
            stack.Enqueue(Dark);
            stack.Enqueue(Light);

            stack.Dequeue().Color.Should().Be(Color.Dark);
            stack.Dequeue().Color.Should().Be(Color.Light);
        }

        [Fact]
        public void StoneStack_Enqueue_Pop_Test()
        {
            var stack = new StoneStack();
            stack.Enqueue(Dark);
            stack.Enqueue(Light);

            stack.Pop().Color.Should().Be(Color.Light);
            stack.Pop().Color.Should().Be(Color.Dark);
        }
    }
}
