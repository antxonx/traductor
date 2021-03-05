namespace Lexico
{
    /// <summary>
    /// tipos de símbolos
    /// </summary>
    internal enum LexType
    {
        IDENTIFIER,
        INTEGER,
        REAL,
        STRING,
        TYPE,
        OP_MATH_ADD,
        OP_MATH_MUL,
        OP_COMP_REL,
        OP_LOGIC_OR,
        OP_LOGIC_AND,
        OP_LOGIC_NOT,
        OP_COMP_EQ,
        SEMI_COLON,
        COMMA,
        OPEN_PAR,
        CLOSE_PAR,
        OPEN_BR,
        CLOSE_BR,
        ASIGNMENT,
        RES_IF,
        RES_WHILE,
        RES_RETURN,
        RES_ELSE,
        END_OF_FILE,
        UNDEFINED
    }

    /// <summary>
    /// Símbolos disponibles
    /// </summary>
    internal class Symbol
    {
        public static readonly string DIGIT_CHARSET = "0123456789";

        public static readonly string TEXT_CHARSET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly string[] RES_TYPES = { "int", "float", "void" };

        public static readonly string[] RES_CONTROL = { "if", "else", "while", "return" };

        public static readonly char STRING_DELIMITER = '\"';

        public static readonly string PAIR_PAR_CHARSET = "()";

        public static readonly string PAIR_BR_CHARSET = "{}";

        public static readonly string OP_MATH_ADD_CHARTSET = "+-";

        public static readonly string OP_MATH_MUL_CHARSET = "*/";

        public static readonly string OP_COMP_REL_START_CHARSET = "<>";

        public static readonly char OP_LOGIC_OR = '|';

        public static readonly char OP_LOGIC_AND = '&';

        public static readonly char OP_LOGIC_NOT = '!';

        public static readonly string[] OP_COMP_EQ = { "==", "!=" };

        public static readonly char SEMI_COLON = ';';

        public static readonly char COMMA = ',';

        public static readonly char ASIGNMENT = '=';

        public static readonly char SPACE_CHAR = ' ';

        public static readonly char END_OF_FILE = '$';
    }

    internal class LexTypeComplex
    {
        public static readonly LexType[] PAIR_PAR = { LexType.OPEN_PAR, LexType.CLOSE_PAR };
        public static readonly LexType[] PAIR_BR = { LexType.OPEN_BR, LexType.CLOSE_BR };
        public static readonly LexType[] RES_CONTROL = { LexType.RES_IF, LexType.RES_ELSE, LexType.RES_WHILE, LexType.RES_RETURN};
    }
}