using System;
using Lexico;

namespace AnalizadorSintactico1
{
    class Program
    {
        static void Main(string[] args)
        {
            LexType lexType;
            string symbol;
            int[][] E =
            {

            };
            Analizer analizer = new Analizer("a+b$");
            while (!analizer.IsDone())
            {
                lexType = analizer.GetNextType();
                symbol = analizer.GetRetSymbol();
                Console.WriteLine($"| {symbol} | {(int)lexType} | {lexType} |");
            }
        }
    }
}
