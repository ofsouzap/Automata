using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
    public abstract class Transition<Symbol> where Symbol : IEquatable<Symbol>
    {

        public int startState;
        public int endState;
        public Symbol sym;

        public Transition(int startState, int endState, Symbol sym)
        {
            this.startState = startState;
            this.endState = endState;
            this.sym = sym;
        }

    }
}
