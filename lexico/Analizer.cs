namespace Lexico
{
    internal class Analizer
    {
        public static readonly string DIGIT_CHARSET = "0123456789";

        public static readonly string TEXT_CHARSET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly char NULL_CHAR = (char)0;

        public static readonly char DECIMAL_POINT_CHAR = '.';

        private string symbol;

        private int index;

        public Analizer()
        {
            this.index = -1;
        }

        public Analizer(string symbol)
        {
            this.symbol = symbol;
            this.index = -1;
        }

        public void SetNewSymbol(string symbol)
        {
            this.symbol = symbol;
            this.index = -1;
        }

        public char GetActualChar()
        {
            int index = (this.index < 0) ? 0 : this.index;
            return this.symbol[index];
        }

        public char NextChar()
        {
            if (!this.IsDone())
            {
                this.index++;
                return this.GetActualChar();
            }
            else
            {
                return Analizer.NULL_CHAR;
            }
        }

        public bool IsDone()
        {
            return this.index >= (this.symbol.Length - 1);
        }

        public string GetSymbol()
        {
            return this.symbol;
        }

        public LexType GetLexType()
        {
            return this.GetNextState(StatePos.Q0, this.NextChar());
        }

        public LexType GetNextState(in StatePos state, in char entry)
        {
            switch (state)
            {
                case StatePos.Q0:
                    if (Analizer.DIGIT_CHARSET.Contains(entry))
                    {
                        return this.GetNextState(StatePos.Q1, this.NextChar());
                    }
                    else if (Analizer.TEXT_CHARSET.Contains(entry))
                    {
                        return this.GetNextState(StatePos.Q4, this.NextChar());
                    }
                    break;

                case StatePos.Q1:
                    if (entry.Equals(Analizer.NULL_CHAR))
                    {
                        return LexType.INTEGER;
                    }
                    else
                    {
                        if (Analizer.DIGIT_CHARSET.Contains(entry))
                        {
                            return this.GetNextState(StatePos.Q1, this.NextChar());
                        }
                        else if (entry.Equals('.'))
                        {
                            return this.GetNextState(StatePos.Q2, this.NextChar());
                        }
                    }
                    break;

                case StatePos.Q2:
                    if (Analizer.DIGIT_CHARSET.Contains(entry))
                    {
                        return this.GetNextState(StatePos.Q3, this.NextChar());
                    }
                    break;

                case StatePos.Q3:
                    if (entry.Equals(Analizer.NULL_CHAR))
                    {
                        return LexType.REAL;
                    }
                    else
                    {
                        if (Analizer.DIGIT_CHARSET.Contains(entry))
                        {
                            return this.GetNextState(StatePos.Q3, this.NextChar());
                        }
                    }
                    break;

                case StatePos.Q4:
                    if (entry.Equals(Analizer.NULL_CHAR))
                    {
                        return LexType.IDENTIFIER;
                    }
                    else
                    {
                        if (Analizer.DIGIT_CHARSET.Contains(entry))
                        {
                            return this.GetNextState(StatePos.Q4, this.NextChar());
                        }
                        else if (Analizer.TEXT_CHARSET.Contains(entry))
                        {
                            return this.GetNextState(StatePos.Q4, this.NextChar());
                        }
                    }
                    break;
            }
            return LexType.UNDEFINED;
        }
    }
}