namespace Lexico
{
    public enum LexType
    {
        INTEGER,
        REAL,
        IDENTIFIER,
        UNDEFINED,
    }

    internal enum StatePos
    {
        Q0,
        Q1, // aceptación
        Q2,
        Q3, // aceptación
        Q4  // aceptación
    }
}