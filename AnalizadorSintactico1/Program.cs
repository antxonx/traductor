using System;
using System.Collections;
using Lexico;

namespace AnalizadorSintactico1
{
    class Program
    {

        internal enum ArrayPos
        {
            IDENTIFIER,
            OP_MATH_ADD,
            END_OF_FILE,
            RULE_E,
            FINISHED
        }

        internal enum TABLE_RULES
        {
            BAD = 0,
            D2 = 2,
            D3 = 3,
            D4 = 4,
            ACC = -1,
            R1 = -3,
            E1 = 1
        }

        static readonly TABLE_RULES[,] LR = new TABLE_RULES[,]
            {
                { TABLE_RULES.D2, TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.E1 },
                { TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.ACC, TABLE_RULES.BAD },
                { TABLE_RULES.BAD, TABLE_RULES.D3, TABLE_RULES.BAD, TABLE_RULES.BAD },
                { TABLE_RULES.D4, TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.BAD },
                { TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.R1, TABLE_RULES.BAD }
            };

        static void Main(string[] args)
        {
            LexType lexType;
            string symbol;
            Stack stack = new Stack();
            Analizer analizer = new Analizer("a+b$");
            stack.Push(0);
            try
            {
                while (!analizer.IsDone())
                {
                    lexType = analizer.GetNextType();
                    symbol = analizer.GetRetSymbol();
                    Process(lexType, symbol, ref stack);
                }
                Process(LexType.END_OF_FILE, "$", ref stack);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void Process(in LexType lexType, in string symbol, ref Stack stack)
        {
            int yPos = int.Parse(stack.Peek().ToString());
            int index = lexType switch
            {
                LexType.IDENTIFIER => (int)ArrayPos.IDENTIFIER,
                LexType.OP_MATH_ADD => (int)ArrayPos.OP_MATH_ADD,
                LexType.END_OF_FILE => (int)ArrayPos.END_OF_FILE,
                _ => throw new Exception("1"),
            };
            TABLE_RULES tableRule = LR[yPos, index];
            int stackStep = (int)tableRule;
            if (stackStep > 0)
            {
                stack.Push(symbol);
                stack.Push(stackStep);
            }
            else if (stackStep < 0)
            {
                for (index = 0; index < stackStep * -2; index++)
                {
                    stack.Pop();
                }
                yPos = int.Parse(stack.Peek().ToString());
                index = tableRule switch
                {
                    TABLE_RULES.R1 => (int)ArrayPos.RULE_E,
                    TABLE_RULES.ACC => (int)ArrayPos.FINISHED,
                    _ => throw new Exception("3"),
                };
                if(index == (int)ArrayPos.FINISHED)
                {
                    PrintProcess(stack);
                    return;
                }
                tableRule = LR[yPos, index];
                stack.Push(tableRule.ToString()[0]);
                stack.Push((int)tableRule);
            }
            else
            {
                throw new Exception("2");
            }
            PrintProcess(stack);
        }

        static void PrintProcess(in Stack stack)
        {
            Object[] revStack = stack.ToArray();
            Array.Reverse(revStack);
            foreach (Object obj in revStack)
                Console.Write("{0}", obj);
            Console.WriteLine();
        }

    }
}
