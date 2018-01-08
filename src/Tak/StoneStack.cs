using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tak
{
    public class StoneStackold : Stack<Piece>
    {
        public int Size => this.Count();

        public StoneStack SplitStack(int take)
        {
            var tempStack = new StoneStack();
            for (int i = 0; i < take; i++)
                tempStack.Push(Pop());

            var newStack = new StoneStack();
            for (int i = 0; i < take; i++)
                newStack.Push(tempStack.Pop());

            return newStack;
        }

        public override string ToString()
        {
            if (!this.Any())
                return "E";
            var sb = new StringBuilder();
            foreach (var piece in this)
                sb.Append(piece.ToString());
            return sb.ToString();
        }

        public StoneStack Copy()
        {
            var ret = new StoneStack();
            foreach (var item in this)
                ret.Push(item);
            return ret;
        }
    }

    public class StoneStack : LinkedList<Piece>
    {
        public StoneStack() { }
        public StoneStack(IEnumerable<Piece> src)
            : base(src)
        {

        }

        public int Size => this.Count();


        /*
         * 
         * LD should represent L on top of D
         * so when Pushing D onto L, It should go first
         * 
         */
        public void Push(Piece p)
        {
            AddFirst(p);
        }

        public Piece Pop()
        {
            var first = First;
            RemoveFirst();
            return first.Value;
        }

        public void Enqueue(Piece p)
        {
            AddFirst(p);
        }

        public Piece Dequeue()
        {
            var last = Last;
            RemoveLast();
            return last.Value;
        }

        public static (StoneStack bottom, StoneStack top) Split(StoneStack stack, int take)
        {
            var bottom = new StoneStack(stack.Take(stack.Count() - take));
            var top = new StoneStack(stack.Take(take));
            return (bottom, top);
        }

        public StoneStack SplitStack(int take)
        {
            var tempStack = new StoneStack();
            for (int i = 0; i < take; i++)
                tempStack.Push(Pop());

            var newStack = new StoneStack();
            for (int i = 0; i < take; i++)
                newStack.Push(tempStack.Pop());

            return newStack;
        }

        public override string ToString()
        {
            if (!this.Any())
                return "E";
            var sb = new StringBuilder();
            foreach (var piece in this)
                sb.Append(piece.ToString());
            return sb.ToString();
        }

        public StoneStack Copy()
        {
            var ret = new StoneStack();
            foreach (var item in this)
                ret.Push(item);
            return ret;
        }
    }

}
