using System;

namespace Automata
{
    public class TuringTransition<Symbol> : Transition<Symbol>
        where Symbol : IEquatable<Symbol>
    {

        public readonly OptSymbol<Symbol> writeSym;

        /// <summary>
        /// How far right the head should move. If negative, moves left. If 0, halts machine
        /// </summary>
        public readonly int headMove;

        public TuringTransition(int startState,
            int endState,
            OptSymbol<Symbol> readSym,
            OptSymbol<Symbol> writeSym,
            int headMove)
            : base(startState, endState, readSym)
        {
            
            this.writeSym = writeSym;
            this.headMove = headMove;

        }

    }
}
