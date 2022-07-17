using System;
using System.Collections.Immutable;

namespace Automata
{
    public class DeterministicPushdownAutomaton<Symbol> : DeterministicAutomaton<PushdownTransition<Symbol>, Symbol>
        where Symbol : IEquatable<Symbol>
    {

        protected readonly Symbol stackBottomSymbol;

        public DeterministicPushdownAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            PushdownTransition<Symbol>[] transitions,
            Symbol stackBottomSymbol)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {
            this.stackBottomSymbol = stackBottomSymbol;
        }

        protected override bool CheckTransitionsAreDeterministic()
        {

            // Tuple is (startState, mainTapeSymbolIsUsed, mainTapeSymbolRead, stackSymbolRead)
            HashSet<(int, bool, Symbol, Symbol)> transitionCombsUsed = new();

            foreach (PushdownTransition<Symbol> t in transitions)
            {

                (int, bool, Symbol, Symbol) comb = (t.startState, !t.dontReadMainTape, t.sym, t.stackSym);
                
                if (transitionCombsUsed.Any(x =>
                    x.Item1 == comb.Item1 // Check start states match
                    && (
                        !x.Item2 && !comb.Item2 // Check both don't read main tape symbol...
                        || (x.Item2 && comb.Item2 && x.Item3.Equals(comb.Item3)) // ...or both read the same main tape symbol
                    )
                    && x.Item4.Equals(comb.Item4) // Check stack symbols read match
                ))
                {
                    return false;
                }

                transitionCombsUsed.Add(comb);

            }

            return true;

        }

        protected PushdownTransition<Symbol>? FindReadTransition(int startState, Symbol sym, Symbol stackSym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && !t.dontReadMainTape && t.sym.Equals(sym) && t.stackSym.Equals(stackSym), null);

        protected PushdownTransition<Symbol>? FindInstantTransition(int startState, Symbol stackSym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && t.dontReadMainTape && t.stackSym.Equals(stackSym), null);

        public override bool Run(Symbol[] word)
        {

            int currentState = 0;
            Queue<Symbol> symQueue = new(word);

            Stack<Symbol> stack = new();
            stack.Push(stackBottomSymbol);

            while (true)
            {

                Symbol stackSym = stack.Pop();

                #region Instant Transitions

                PushdownTransition<Symbol>? insT = FindInstantTransition(currentState, stackSym);

                if (insT != null)
                {

                    // Write transition word
                    foreach (Symbol s in insT.stackReplacement)
                        stack.Push(s);

                    // Change state
                    currentState = insT.endState;

                    // Restart loop
                    continue;

                }

                #endregion

                // Check if symbol available to read from word

                if (symQueue.Count == 0)
                    break;

                #region Main Tape-Reading Transitions

                PushdownTransition<Symbol>? readT = FindReadTransition(currentState, symQueue.Dequeue(), stackSym);

                if (readT == null)
                    return false;

                // Write transition word
                foreach (Symbol s in readT.stackReplacement)
                    stack.Push(s);

                // Change state
                currentState = readT.endState;

                #endregion

            }

            return acceptingStates.Contains(currentState);

        }

    }
}
