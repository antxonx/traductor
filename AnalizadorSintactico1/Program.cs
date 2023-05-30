using Lexico;
using System;
using System.Collections;

namespace AnalizadorSintactico1
{
    internal class Program
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

        private static readonly TABLE_RULES[,] LR = new TABLE_RULES[,]
            {
                { TABLE_RULES.D2, TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.E1 },
                { TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.ACC, TABLE_RULES.BAD },
                { TABLE_RULES.BAD, TABLE_RULES.D3, TABLE_RULES.BAD, TABLE_RULES.BAD },
                { TABLE_RULES.D4, TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.BAD },
                { TABLE_RULES.BAD, TABLE_RULES.BAD, TABLE_RULES.R1, TABLE_RULES.BAD }
            };

        private static void Main(string[] args)
        {
            LexType lexType;
            string symbol, textLeft;
            Stack stack = new Stack();
            Stack stackBack = new Stack();
            Analizer analizer = new Analizer("a+b$");
            stack.Push(0);
            try
            {
                //textLeft = analizer.GetTextLext();
                //PrintProcess(stack, textLeft);
                while (!analizer.IsDone())
                {
                    lexType = analizer.GetNextType();
                    symbol = analizer.GetRetSymbol();
                    textLeft = analizer.GetTextLext();
                    Process(lexType, symbol, textLeft, ref stack, ref stackBack);
                }
                Process(LexType.END_OF_FILE, "$", "$", ref stack, ref stackBack);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void Process(in LexType lexType, in string symbol, in string textLeft, ref Stack stack, ref Stack stackBack)
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
            stackBack = (Stack)stack.Clone();
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
                PrintProcess(stackBack, textLeft, tableRule);
                if (index == (int)ArrayPos.FINISHED)
                {
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
            if(tableRule.ToString()[0] != 'E')
            {
                PrintProcess(stackBack, textLeft, tableRule);
            }
        }

        private static void PrintProcess(in Stack stack, in string textLeft, in TABLE_RULES tableRule)
        {
            Object[] revStack = stack.ToArray();
            string stackText = "", ruleExt;
            Array.Reverse(revStack);
            foreach (Object obj in revStack)
                stackText += obj;
            Console.Write($"{stackText,-10}");
            Console.Write($" | {textLeft, -10}");
            if(tableRule.ToString()[0].Equals('R'))
            {
                switch (tableRule)
                {
                    case TABLE_RULES.R1:
                        ruleExt = tableRule + " E -> <id> + <id>";
                        break;
                    default:
                        throw new Exception("4");
                }
                Console.Write($" | {ruleExt,-30} |");
            } 
            else
            {
                Console.Write($" | {tableRule,-30} |");
            }
            Console.WriteLine();
        }
    }
}