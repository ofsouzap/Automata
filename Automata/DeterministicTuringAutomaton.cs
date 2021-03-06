using System;
using System.Collections.Immutable;

namespace Automata
{
    public class DeterministicTuringAutomaton<Symbol> : DeterministicAutomaton<TuringTransition<Symbol>, Symbol>
        where Symbol : IEquatable<Symbol>
    {

        private readonly Symbol blankSymbol;

        public DeterministicTuringAutomaton(ImmutableHashSet<Symbol> alphabet,
            int stateCount,
            ImmutableHashSet<int> acceptingStates,
            TuringTransition<Symbol>[] transitions,
            Symbol blankSymbol)
            : base(alphabet, stateCount, acceptingStates, transitions)
        {
            this.blankSymbol = blankSymbol;
        }

        public class Tape
        {

            private readonly Symbol blankSymbol;
            private readonly Dictionary<int, Symbol> values;

            private IEnumerable<int> NonBlankValueKeys
                => values
                .Where(p => !p.Value.Equals(blankSymbol))
                .Select(p => p.Key);

            protected int NonBlankStartIndex
                => NonBlankValueKeys.Min();

            protected int NonBlankEndIndex
                => NonBlankValueKeys.Max();

            public Tape(Symbol blankSymbol)
            {
                values = new Dictionary<int, Symbol>();
                this.blankSymbol = blankSymbol;
            }

            public Tape(Symbol blankSymbol, Symbol[] initialValues) : this(blankSymbol)
            {
                for (int i = 0; i < initialValues.Length; i++)
                    SetValue(i, initialValues[i]);
            }

            public Symbol this[int index]
            {
                get => GetValue(index);
                set => SetValue(index, value);
            }

            protected Symbol GetValue(int index)
            {
                if (values.ContainsKey(index))
                    return values[index];
                else
                    return blankSymbol;
            }

            protected void SetValue(int index, Symbol value)
            {
                if (values.ContainsKey(index))
                    values[index] = value;
                else
                    values.Add(index, value);
            }

            /// <summary>
            /// Reads the tape from the first non-blank symbol to the last non-blank symbol
            /// </summary>
            public IEnumerable<Symbol> Read()
            {

                if (NonBlankValueKeys.Count() == 0)
                    yield break;

                int end = NonBlankEndIndex; // Caching for efficiency
                for (int i = NonBlankStartIndex; i <= end; i++)
                    yield return this[i];

            }

            public bool CompareToArray(Symbol[] a)
            {

                int i = 0;

                foreach (Symbol s in Read())
                    if (!a[i++].Equals(s))
                        return false;

                // Still can be false if len(a) > len(tapeUsed)
                return i == a.Length;

            }

        }

        protected override bool CheckTransitionsAreDeterministic()
        {

            HashSet<(int, Symbol?)> stateSymPairsUsed = new();

            foreach (TuringTransition<Symbol> t in transitions)
            {

                (int, Symbol?) stateSymPair = (t.startState, t.sym);

                if (stateSymPairsUsed.Contains(stateSymPair))
                    return false;

                stateSymPairsUsed.Add(stateSymPair);

            }

            return true;

        }

        protected TuringTransition<Symbol>? FindInstantTransition(int startState)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && !t.sym.Exists, null);

        protected TuringTransition<Symbol>? FindReadTransition(int startState, Symbol sym)
            => GetTransitionsFromState(startState).FirstOrDefault(t => t != null && t.sym.Exists && t.sym.Equals(sym), null);

        public bool Run(Symbol[] word, out Tape tape)
        {

            int currentState = 0;

            int headPosition = 0;
            tape = new(blankSymbol, word);

            while (true)
            {

                #region Instant Transitions

                TuringTransition<Symbol>? insT = FindInstantTransition(currentState);

                if (insT != null)
                {

                    currentState = insT.endState;

                    if (insT.writeSym.Exists)
                        tape[headPosition] = insT.writeSym;

                    if (insT.headMove == 0)
                        break;
                    else
                    {
                        headPosition += insT.headMove;
                        continue;
                    }

                }

                #endregion

                #region Read Transitions

                Symbol sym = tape[headPosition];

                TuringTransition<Symbol>? t = FindReadTransition(currentState, sym);

                if (t != null)
                {

                    currentState = t.endState;

                    if (t.writeSym.Exists)
                        tape[headPosition] = t.writeSym;

                    if (t.headMove == 0)
                        break;
                    else
                    {
                        headPosition += t.headMove;
                        continue;
                    }

                }

                #endregion

                // If no transitions were found, the word is rejected
                return false;

            }

            // Return whether the automaton has ended in an accepting state
            return acceptingStates.Contains(currentState);

        }

        public override bool Run(Symbol[] word)
            => Run(word, out _);

    }
}
