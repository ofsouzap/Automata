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

            // Tuple is (startState, mainTapeSymbolRead, stackSymbolRead)
            HashSet<(int, OptSymbol<Symbol>, Symbol)> transitionCombsUsed = new();

            foreach (PushdownTransition<Symbol> t in transitions)
            {

                (int, OptSymbol<Symbol>, Symbol) comb = (t.startState, t.sym, t.stackSym);
                
                if (transitionCombsUsed.Contains(comb))
                    return false;

                transitionCombsUsed.Add(comb);

            }

            return true;

        }

        protected PushdownTransition<Symbol>? FindInstantTransition(int startState, Symbol stackSym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && !t.sym.Exists && t.stackSym.Equals(stackSym), null);

        protected PushdownTransition<Symbol>? FindReadTransition(int startState, Symbol sym, Symbol stackSym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && t.sym.Exists && t.sym.Equals(sym) && t.stackSym.Equals(stackSym), null);

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

                    continue;

                }

                #endregion

                // Check if symbol available to read from word

                if (symQueue.Count == 0)
                    break;

                #region Main Tape-Reading Transitions

                PushdownTransition<Symbol>? readT = FindReadTransition(currentState, symQueue.Dequeue(), stackSym);

                if (readT != null)
                {

                    // Write transition word
                    foreach (Symbol s in readT.stackReplacement)
                        stack.Push(s);

                    // Change state
                    currentState = readT.endState;

                    continue;

                }

                #endregion

                // If no transitions were found, the word is rejected
                return false;

            }

            return acceptingStates.Contains(currentState);

        }

    }
}
