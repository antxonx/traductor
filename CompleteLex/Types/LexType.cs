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
        COLON,
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
    /// Estados del autoamata
    /// </summary>
    internal enum StatePos
    {
        Q0,
        Q1,
        Q2,
        Q3,
        Q4
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

        public static readonly string[] PAIR_STRING = { "\"", "\"" };

        public static readonly string[] PAIR_PAR = { "(", ")" };

        public static readonly string[] PAIR_BR = { "{", "}" };

        public static readonly string[] OP_MATH_ADD = { "+", "-" };

        public static readonly string[] OP_MATH_MUL = { "*", "/" };

        public static readonly string[] OP_COMP_REL = { "<", "<=", ">=", ">" };

        public static readonly string OP_LOGIC_OR = "||";

        public static readonly string OP_LOGIC_AND = "&&";

        public static readonly string OP_LOGIC_NOT = "!";

        public static readonly string[] OP_COMP_EQ = { "==", "!=" };

        public static readonly string SEMI_COLON = ";";

        public static readonly string COLON = ",";

        public static readonly string ASIGNMENT = "=";

        public static readonly string END_OF_FILE = "$";

    }
}