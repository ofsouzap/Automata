using System;
using System.Collections.Generic;

namespace Automata
{
    public class PushdownTransition<Symbol> : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public readonly Symbol stackSym;
        public readonly Symbol[] stackReplacement;

        /// <summary>
        /// Whether the transition doesn't require reading a symbol from the main tape
        /// </summary>
        public readonly bool dontReadMainTape;

        public PushdownTransition(int startState, int endState, Symbol sym, Symbol stackSym, Symbol[] stackReplacement) : base(startState, endState, sym)
        {

            this.stackSym = stackSym;
            this.stackReplacement = stackReplacement;
            dontReadMainTape = false;

        }

        public PushdownTransition(int startState, int endState, Symbol stackSym, Symbol[] stackReplacement) : base(startState, endState, default)
        {

            // This constructor is for transitions not requiring reading the main tape

            this.stackSym = stackSym;
            this.stackReplacement = stackReplacement;
            dontReadMainTape = true;

        }

    }
}
