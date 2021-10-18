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

            int size = ((int)Math.Sqrt(field.Length));

            Cell[,] fieldWithBoard = new Cell[size + 2, size + 2];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    fieldWithBoard[i + 1, j + 1] = field[i, j];
                }
            }

            //заполнение границ игрового поля
            for (int i = 1; i < ((int)Math.Sqrt(fieldWithBoard.Length)) - 1; i++)
            {
                fieldWithBoard[0, i] = new Cell(MapObject.boardMapHorizontal);
                fieldWithBoard[((int)Math.Sqrt(fieldWithBoard.Length)) - 1, i] = new Cell(MapObject.boardMapHorizontal);
                fieldWithBoard[i, 0] = new Cell(MapObject.boardMapVertical);
                fieldWithBoard[i, ((int)Math.Sqrt(fieldWithBoard.Length)) - 1] = new Cell(MapObject.boardMapVertical);
            }
            fieldWithBoard[0, 0] = new Cell(MapObject.boardMapAngleTopLeft);
            fieldWithBoard[0, ((int)Math.Sqrt(fieldWithBoard.Length)) - 1] = new Cell(MapObject.boardMapAngleTopRight);
            fieldWithBoard[((int)Math.Sqrt(fieldWithBoard.Length)) - 1, 0] = new Cell(MapObject.boardMapAngleBotLeft);
            fieldWithBoard[((int)Math.Sqrt(fieldWithBoard.Length)) - 1, ((int)Math.Sqrt(fieldWithBoard.Length)) - 1] = new Cell(MapObject.boardMapAngleBotRight);

            for (int i = 0; i < Math.Sqrt(fieldWithBoard.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(fieldWithBoard.Length); j++)
                {
                    Console.Write($"{((char)(fieldWithBoard[i,j].Value))}");
                }
                Console.WriteLine();
            }
        }
    }
}
