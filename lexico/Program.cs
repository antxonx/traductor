using System;

namespace Lexico
{
    class Program
    {

        public const int LEFT_COL_SIZE = 30;
        public const int RIGHT_COL_SIZE = 50;

        static void Main(string[] args)
        {
            string[] dataSet = { "901", "90.1", "67231", "0.31231", "gwegfw", "55.", "33.333.3", "4.5", "swe23131", "dwf231.31", "asd_efw" };
            int index;

            Analizer analizer = new Analizer();
            /**
             * Header
             */
            Console.WriteLine($"| {"Symbol", -LEFT_COL_SIZE} | {"Type", -RIGHT_COL_SIZE}");
            Console.Write("|");
            for(index = 0; index <= LEFT_COL_SIZE + 1; index++)
            {
                Console.Write("-");
            }
            Console.Write("|");
            for (index = 0; index <= RIGHT_COL_SIZE + 1; index++)
            {
                Console.Write("-");
            }
            /**
             */
            Console.WriteLine();
            index = 0;
            while (index < dataSet.Length)
            {
                try
                {
                    analizer.SetNewSymbol(dataSet[index]);
                    //Console.WriteLine(analizer.GetSymbol() + " \t\t\t " + analizer.GetLexType());
                    Console.WriteLine($"| {analizer.GetSymbol(), -LEFT_COL_SIZE} | {analizer.GetLexType(), -RIGHT_COL_SIZE}");
                } catch(LexTypeException e)
                {
                    //Console.WriteLine(analizer.GetSymbol() + "\t ERROR: " + e.Message);
                    Console.WriteLine($"| {analizer.GetSymbol(), -LEFT_COL_SIZE} | ERROR: {e.Message}");
                }
                index++;
            }
        }
    }
}
