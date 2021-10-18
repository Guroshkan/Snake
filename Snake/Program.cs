using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            SnakeGame snakeGame = new SnakeGame();
            snakeGame.StartGame();
            Console.WriteLine($"Ваш результат такой {0}");
        }
    }
}
