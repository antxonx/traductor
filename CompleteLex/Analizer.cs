using System;
using System.Linq;

namespace Lexico
{
    public class Analizer
    {
        public static readonly char NULL_CHAR = (char)0;

        private string file;

        private int index;

        private string symbol;

        public Analizer(in string file)
        {
            this.file = file;
            this.index = 0;
            this.symbol = "";
        }

        public char Continue(in char entry)
        {
            this.symbol += entry;
            this.index++;
            return this.GetActualChar();
        }

        public LexType GetNextType()
        {
            return this.Start(this.GetActualChar());
        }

        public string GetTextLext()
        {
            if(this.IsDone())
            {
                return "" + Symbol.END_OF_FILE;
            } else
            {
                return this.file[(this.index-1)..];
            }
        }

        public string GetRetSymbol()
        {
            return this.symbol;
        }

        public bool IsDone()
        {
            return this.index >= this.file.Length;
        }

        public Analizer SetFile(in string file)
        {
            this.file = file;
            this.index = 0;
            this.symbol = "";
            return this;
        }

        private LexType AcceptId(in char entry)
        {
            int aux;
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptId(this.Continue(entry));
            }
            else if (Symbol.TEXT_CHARSET.Contains(entry))
            {
                return this.AcceptId(this.Continue(entry));
            }
            if(Symbol.RES_TYPES.Contains(this.symbol))
            {
                return LexType.TYPE;
            } else  if(Symbol.RES_CONTROL.Contains(this.symbol))
            {
                aux = Array.IndexOf(Symbol.RES_CONTROL, this.symbol);
                return LexTypeComplex.RES_CONTROL[aux];
            }
            return LexType.IDENTIFIER;
        }

        private LexType AcceptInteger(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptInteger(this.Continue(entry));
            }
            else if (entry.Equals('.'))
            {
                return this.DecimalPoint(this.Continue(entry));
            }
            return LexType.INTEGER;
        }

        private LexType AcceptReal(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptReal(this.Continue(entry));
            }
            return LexType.REAL;
        }

        private LexType DecimalPoint(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptReal(this.Continue(entry));
            }
            return LexType.UNDEFINED;
        }

        private char GetActualChar()
        {
            int index = (this.index < 0) ? 0 : this.index;
            if (this.IsDone())
            {
                return Analizer.NULL_CHAR;
            }
            else
            {
                return this.file[index];
            }
        }
        private LexType OpCompRelAccept(in char entry)
        {
            if (entry.Equals(Symbol.ASIGNMENT))
            {
                this.Continue(entry);
            }
            return LexType.OP_COMP_REL;
        }

        private LexType StringAccept(in char entry)
        {
            if(entry.Equals(Symbol.STRING_DELIMITER))
            {
                this.Continue(entry);
                return LexType.STRING;
            } 
            else if(entry.Equals(Analizer.NULL_CHAR))
            {
                this.Continue(entry);
                return LexType.UNDEFINED;
            } 
            else {
                return this.StringAccept(this.Continue(entry));
            }
        }

        private LexType AcceptLogicNotOrCompEq(in char entry)
        {
            if (entry.Equals(Symbol.ASIGNMENT))
            {
                this.Continue(entry);
                return LexType.OP_COMP_EQ;
            }
            return LexType.OP_LOGIC_NOT;
        }

        private LexType AcceptAssignmentOrCompEq(in char entry)
        {
            if (entry.Equals(Symbol.ASIGNMENT))
            {
                this.Continue(entry);
                return LexType.OP_COMP_EQ;
            }
            return LexType.ASIGNMENT;
        }

        private LexType AcceptOpLogicOr(in char entry)
        {
            if (entry.Equals(Symbol.OP_LOGIC_OR))
            {
                this.Continue(entry);
                return LexType.OP_LOGIC_OR;
            }
            return LexType.UNDEFINED;
        }

        private LexType AcceptOpLogicAnd(in char entry)
        {
            if (entry.Equals(Symbol.OP_LOGIC_AND))
            {
                this.Continue(entry);
                return LexType.OP_LOGIC_AND;
            }
            return LexType.UNDEFINED;
        }

        private LexType Start(in char entry)
        {
            this.symbol = "";
            int aux;
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptInteger(this.Continue(entry));
            }
            else if (Symbol.TEXT_CHARSET.Contains(entry))
            {
                return this.AcceptId(this.Continue(entry));
            }
            else if (Symbol.PAIR_PAR_CHARSET.Contains(entry))
            {
                aux = Symbol.PAIR_PAR_CHARSET.IndexOf(entry);
                this.Continue(entry);
                return LexTypeComplex.PAIR_PAR[aux];
            }
            else if (Symbol.PAIR_BR_CHARSET.Contains(entry))
            {
                aux = Symbol.PAIR_BR_CHARSET.IndexOf(entry);
                this.Continue(entry);
                return LexTypeComplex.PAIR_BR[aux];
            }
            else if (Symbol.OP_MATH_ADD_CHARTSET.Contains(entry))
            {
                this.Continue(entry);
                return LexType.OP_MATH_ADD;
            }
            else if (entry.Equals(Symbol.STRING_DELIMITER))
            {
                return this.StringAccept(this.Continue(entry));
            }
            else if (Symbol.OP_MATH_MUL_CHARSET.Contains(entry))
            {
                this.Continue(entry);
                return LexType.OP_MATH_MUL;
            }
            else if(Symbol.OP_COMP_REL_START_CHARSET.Contains(entry))
            {
                return this.OpCompRelAccept(this.Continue(entry));
            }
            else if (entry.Equals(Symbol.SEMI_COLON))
            {
                this.Continue(entry);
                return LexType.SEMI_COLON;
            }
            else if (entry.Equals(Symbol.COMMA))
            {
                this.Continue(entry);
                return LexType.COMMA;
            }
            else if (entry.Equals(Symbol.ASIGNMENT))
            {
                return this.AcceptAssignmentOrCompEq(this.Continue(entry));
            }
            else if (entry.Equals(Symbol.OP_LOGIC_NOT))
            {
                return this.AcceptLogicNotOrCompEq(this.Continue(entry));
            }
            else if (entry.Equals(Symbol.END_OF_FILE))
            {
                this.Continue(entry);
                return LexType.END_OF_FILE;
            }
            else if(entry.Equals(Symbol.OP_LOGIC_OR))
            {
                return this.AcceptOpLogicOr(this.Continue(entry));
            }
            else if (entry.Equals(Symbol.OP_LOGIC_AND))
            {
                return this.AcceptOpLogicAnd(this.Continue(entry));
            }
            else if (entry.Equals(Symbol.SPACE_CHAR))
            {
                return this.Start(this.Continue(entry));
            }
            this.Continue(entry);
            return LexType.UNDEFINED;
        }
    }
}