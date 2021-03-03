namespace Lexico
{
    internal class Analizer
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

        public Analizer SetFile(in string file)
        {
            this.file = file;
            this.index = 0;
            this.symbol = "";
            return this;
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

        public string GetRetSymbol()
        {
            return this.symbol;
        }

        public LexType GetNextType()
        {
            return this.Start(this.GetActualChar());
        }

        public bool IsDone()
        {
            return this.index >= this.file.Length;
        }

        public char Continue(in char entry)
        {
            this.symbol += entry;
            this.index++;
            return this.GetActualChar();
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
            else if (entry.Equals(Symbol.SEMI_COLON))
            {
                this.Continue(entry);
                return LexType.SEMI_COLON;
            }
            else if (entry.Equals(Symbol.COLON))
            {
                this.Continue(entry);
                return LexType.COLON;
            }
            else if (entry.Equals(Symbol.ASIGNMENT))
            {
                this.Continue(entry);
                return LexType.ASIGNMENT;
            }
            else if (entry.Equals(Symbol.OP_LOGIC_NOT))
            {
                this.Continue(entry);
                return LexType.OP_LOGIC_NOT;
            }
            else if (entry.Equals(Symbol.SPACE_CHAR))
            {
                return this.Start(this.Continue(entry));
            }
            this.Continue(entry);
            return LexType.UNDEFINED;
        }

        private LexType AcceptInteger(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(this.GetActualChar()))
            {
                return this.AcceptInteger(this.Continue(entry));
            } else if(this.GetActualChar().Equals('.'))
            {
                return this.DecimalPoint(this.Continue(entry));
            }
            return LexType.INTEGER;
        }

        private LexType DecimalPoint(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptReal(this.Continue(entry));
            }
            return LexType.UNDEFINED;
        }

        private LexType AcceptReal(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptReal(this.Continue(entry));
            }
            return LexType.REAL;
        }

        private LexType AcceptId(in char entry)
        {
            if (Symbol.DIGIT_CHARSET.Contains(entry))
            {
                return this.AcceptId(this.Continue(entry));
            }
            else if(Symbol.TEXT_CHARSET.Contains(entry))
            {
                return this.AcceptId(this.Continue(entry));
            }
            return LexType.IDENTIFIER;
        }
    }
}