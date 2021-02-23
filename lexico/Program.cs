using System;
using System.Collections.Generic;

namespace Lexico
{
    class Program
    {
        public const int LEFT_COL_SIZE = 30;

        public const int RIGHT_COL_SIZE = 30;

        static void Main(string[] args)
        {
            string entryPath = Environment.CurrentDirectory + "/entry.txt";
            string file = System.IO.File.ReadAllText(entryPath);
            int index;
            string symbol = "";
            string lastSymbol = symbol;
            bool done = false;
            bool start = false;
            List<string> errors = new List<string>();
            file = file.Replace("\n", " ").Replace("\r", " ");
            PrintTableLimit();
            Console.WriteLine($"| {"Symbol",-LEFT_COL_SIZE} | {"Type",-RIGHT_COL_SIZE}|");
            Console.Write("|");
            for (index = 0; index <= LEFT_COL_SIZE + 1; index++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            for (index = 0; index <= RIGHT_COL_SIZE; index++)
            {
                Console.Write("-");
            }
            Console.WriteLine("|");
            index = 0;
            while (index < file.Length)
            {
                if (file[index] == ' ')
                {
                    if (start)
                    {
                        done = true;
                        start = false;
                    }
                }
                else
                {
                    if (!start)
                    {
                        start = true;
                    }
                    symbol += file[index];
                }
                if (done)
                {
                    if (symbol != System.Environment.NewLine)
                    {
                        AnalizeSymbol(symbol, ref errors);
                    }
                    lastSymbol = symbol;
                    done = false;
                    symbol = "";
                }
                index++;
            }

            if (symbol.Length > 0 && lastSymbol != symbol)
            {
                if (symbol != System.Environment.NewLine)
                {
                    AnalizeSymbol(symbol, ref errors);
                }
            }
            PrintTableLimit();
            Console.WriteLine("");
            index = 1;
            foreach (string error in errors)
            {
                Console.WriteLine($"ERROR {index}: {error}");
                index++;
            }
        }

        public static void PrintTableLimit()
        {
            int index;
            for (index = 0; index <= RIGHT_COL_SIZE + LEFT_COL_SIZE + 5; index++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
        }

        public static void AnalizeSymbol(in string symbol, ref List<string> errors)
        {
            Analizer analizer = new Analizer();

            try
            {
                analizer.SetNewSymbol(symbol);
                Console.WriteLine($"| {analizer.GetSymbol(),-LEFT_COL_SIZE} | {analizer.GetLexType(),-RIGHT_COL_SIZE}|");
            }
            catch (LexTypeException e)
            {
                errors.Add(e.Message);
                Console.WriteLine($"| {analizer.GetSymbol(),-LEFT_COL_SIZE} | {"!--ERROR " + errors.Count + "--!",-RIGHT_COL_SIZE}|");
            }

        }
    }
}
