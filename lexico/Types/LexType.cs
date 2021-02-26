namespace Lexico
{
    /// <summary>
    /// tipos de símbolos
    /// </summary>
    public enum LexType
    {
        /// <summary>
        /// [0-9]+
        /// </summary>
        INTEGER,
        /// <summary>
        /// [0-9]+.[0-9]+
        /// </summary>
        REAL,
        /// <summary>
        /// [a-zA-Z]([a-zA-Z]|[0-9])*
        /// </summary>
        IDENTIFIER,
        /// <summary>
        /// fallback
        /// </summary>
        UNDEFINED,
    }

    /// <summary>
    /// Estados del autoamata 
    /// </summary>
    internal enum StatePos
    {
        /// <summary>
        /// Inicio
        /// </summary>
        Q0,
        /// <summary>
        /// Aceptar INTEGER
        /// </summary>
        Q1,
        Q2,
        /// <summary>
        /// Aceptar Real
        /// </summary>
        Q3,
        /// <summary>
        /// Aceptar Identificador
        /// </summary>
        Q4
    }
}