using System;
using System.Collections.Generic;
using System.Linq;

namespace Automata
{
    public class FiniteTransition<Symbol> : Transition<Symbol> where Symbol : IEquatable<Symbol>
    {

        public FiniteTransition(int startState, int endState, OptSymbol<Symbol> sym) : base(startState, endState, sym)
        {

        }

    }
}
