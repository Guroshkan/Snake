using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    static class Output
    {
        public static void Show(Map map)
        {
            Console.Clear();
            Cell[,] field = map.GetCopyFieldForOutput();
            for (int i = 0; i < Math.Sqrt(field.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(field.Length); j++)
                {
                    Console.Write($"{((char)(field[i,j].Value))}");
                }
                Console.WriteLine();
            }
        }
    }
}
