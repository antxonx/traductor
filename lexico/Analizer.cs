using System;
using System.Collections.Generic;
using System.Text;

namespace Lexico
{
    class Analizer
    {
        public static readonly string DIGIT_CHARSET = "0123456789";

        public static readonly string LETTER_CHARSET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly char EMPTY_CHAR = (char)0;

        public static readonly char DECIMAL_POINT_CHAR = '.';

        private static readonly string DECIMAL_EXCEPTION_MSG = "Un valor numérico sólo debe incluir digitos y un punto decimal seguido de números -> [0-9]+(.[0-9]+)";

        private static readonly string IDENTIFIER_EXCEPTION_MSG = "Un identificador se conforma por una letra, seguido de digitos o letras opcionales -> [a-zA-Z]([a-zA-Z]|[0-9])*";

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
                return Analizer.EMPTY_CHAR;
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
            return LexType.UNDEFINED;
        }

    }
}