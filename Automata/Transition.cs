using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
    public abstract class Transition<Symbol> where Symbol : IEquatable<Symbol>
    {

        public int startState;
        public int endState;
        public OptSymbol<Symbol> sym;

        public Transition(int startState, int endState, OptSymbol<Symbol> sym)
        {
            this.startState = startState;
            this.endState = endState;
            this.sym = sym;
        }

    }

    public struct OptSymbol<Symbol>
    {

        public static OptSymbol<Symbol> None => new();

        private readonly bool symExists;
        private readonly Symbol? sym;

        public OptSymbol(Symbol sym)
        {
            symExists = true;
            this.sym = sym;
        }

        public OptSymbol()
        {
            symExists = false;
            sym = default;
        }

        public bool Exists => symExists;

        public static implicit operator Symbol(OptSymbol<Symbol> s)
        {

            if (s.symExists == false)
                throw new Exception("Trying to get symbol for OptSymbol without symbol");

            if (s.sym == null)
                throw new Exception("OptSymbol symbol was unexpectedly null");

            return s.sym;

        }
        public static implicit operator OptSymbol<Symbol>(Symbol s) => new(s);

        public override bool Equals([NotNullWhen(true)] object? obj)
        {

            if (obj is OptSymbol<Symbol> objOptSym)
                return (!objOptSym.Exists && !Exists) || (objOptSym.Exists && Exists && objOptSym.sym != null && objOptSym.sym.Equals(sym));
            else if (obj is Symbol objSym)
                return Exists && sym != null && (sym.Equals(objSym));
            else
                return base.Equals(obj);

        }

        public static bool operator ==(OptSymbol<Symbol> left, OptSymbol<Symbol> right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(OptSymbol<Symbol> left, OptSymbol<Symbol> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
            => base.GetHashCode();

    }

}
