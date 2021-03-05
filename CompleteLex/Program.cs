using System;

namespace Lexico
{
    internal class Program
    {
        public const int LEFT_COL_SIZE = 30;

        public const int RIGHT_COL_SIZE = 30;

        public const int MIDDLE_COL_SIZE = 5;

        private static void Main(string[] args)
        {
            string entryPath, file;
            Analizer analizer;
            LexType lexType;
            string symbol;
            int index;
            if (args.Length == 0)
            {
                Console.WriteLine("Des especificar el nombre del archivo al ejecutar el programa");
                return;
            }
            entryPath = Environment.CurrentDirectory + "/" + args[0];
            file = System.IO.File.ReadAllText(entryPath);

            analizer = new Analizer(file.Replace("\n", " ").Replace("\r", " "));
            PrintTableLimit();

            Console.WriteLine($"| {"Symbol",-LEFT_COL_SIZE} | {"Id",-MIDDLE_COL_SIZE} | {"Type",-RIGHT_COL_SIZE} |");
            Console.Write("|");
            for (index = 0; index <= LEFT_COL_SIZE; index++)
            {
                Console.Write("-");
            }
            Console.Write("-|");
            for (index = 0; index <= MIDDLE_COL_SIZE; index++)
            {
                Console.Write("-");
            }
            Console.Write("-|");
            for (index = 0; index <= RIGHT_COL_SIZE; index++)
            {
                Console.Write("-");
            }
            Console.WriteLine("-|");
            while (!analizer.IsDone())
            {
                lexType = analizer.GetNextType();
                symbol = analizer.GetRetSymbol();
                Console.WriteLine($"| {symbol,-LEFT_COL_SIZE} | {(int)lexType,MIDDLE_COL_SIZE} | {lexType,-RIGHT_COL_SIZE} |");
            }
            PrintTableLimit();
        }

        public static void PrintTableLimit()
        {
            int index;
            for (index = 0; index <= RIGHT_COL_SIZE + MIDDLE_COL_SIZE + LEFT_COL_SIZE + 9; index++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
        }
    }
}