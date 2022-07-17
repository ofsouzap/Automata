using System;
using System.Collections.Generic;

namespace Automata
{
    public class PushdownTransition<Symbol> : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public readonly Symbol stackSym;
        public readonly Symbol[] stackReplacement;

        public PushdownTransition(int startState, int endState, OptSymbol<Symbol> sym, Symbol stackSym, Symbol[] stackReplacement) : base(startState, endState, sym)
        {

            this.stackSym = stackSym;
            this.stackReplacement = stackReplacement;

        }

    }
}
