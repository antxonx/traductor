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
            string[] dataSet = {
                "901",
                "90.1",
                "67231t",
                "0.31231",
                "gwegfw",
                "55.",
                "33.333.3",
                "4.5",
                "swe23131",
                "dwf231.31",
                "asd_efw",
                "aaa3333",
                "else"
            };
            int index;
            List<string> errors = new List<string>();
            Analizer analizer = new Analizer();
            PrintTableLimit();
            /**
             * start table
             */
            Console.WriteLine($"| {"Symbol", -LEFT_COL_SIZE} | {"Type", -RIGHT_COL_SIZE}|");
            Console.Write("|");
            for(index = 0; index <= LEFT_COL_SIZE + 1; index++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            for (index = 0; index <= RIGHT_COL_SIZE; index++)
            {
                Console.Write("-");
            }
            /**
             */
            Console.WriteLine("|");
            index = 0;
            while (index < dataSet.Length)
            {
                try
                {
                    analizer.SetNewSymbol(dataSet[index]);
                    Console.WriteLine($"| {analizer.GetSymbol(), -LEFT_COL_SIZE} | {analizer.GetLexType(), -RIGHT_COL_SIZE}|");
                } catch(LexTypeException e)
                {
                    errors.Add(e.Message);
                    Console.WriteLine($"| {analizer.GetSymbol(), -LEFT_COL_SIZE} | {"!--ERROR " + errors.Count + "--!", -RIGHT_COL_SIZE}|");
                }
                index++;
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
    }
}
