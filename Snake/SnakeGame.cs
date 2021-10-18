
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.Windows.Forms;

namespace Snake
{
    class SnakeGame
    {
        const int SIZEFIELD = 20;
        const int TIMESLEEP = 2000;
        public SnakeGame()
        {
            this._Map = new Map(SIZEFIELD);
            Random random = new Random();
            Point pointOfSnake = this._Map.GetRandomFreeCell();
            _Map.FillCell(pointOfSnake);
            _Snake = new Snake(pointOfSnake);
        }
        private Map _Map;
        private Snake _Snake;
        private int Score;
        private bool IsWin;

        public void StartGame()
        {
            int score = 0;
            bool IsAlive = true;
            this.SpawnNewFood();
            while (IsAlive)
            {
                Point nextStep = new Point(this._Snake.GetHead().GetPosition()._X, this._Snake.GetHead().GetPosition()._Y);
                switch (this._Snake.GetDirection())
                {
                    case SnakeOrientation.UP: nextStep = new Point(nextStep._X, nextStep._Y + 1); break;
                    case SnakeOrientation.DOWN: nextStep = new Point(nextStep._X, nextStep._Y - 1); break;
                    case SnakeOrientation.LEFT: nextStep = new Point(nextStep._X - 1, nextStep._Y); break;
                    case SnakeOrientation.RIGHT: nextStep = new Point(nextStep._X + 1, nextStep._Y); break;
                }
                this.GetKeyDown();
                this.ChangeMap(this._Snake.GetHead());
                Thread.Sleep(TIMESLEEP);
                Output.Show(this._Map);
                if (_Map.GetCountFreeCells() > 0)
                {
                    if (nextStep != this._Snake.GetHead().GetPosition())
                    {
                        for (; ; )
                        {
                            switch (this._Map.GetCellFromField(nextStep).Value)
                            {
                                case MapObject.Food: this._Snake.AddSegment(nextStep); score++; this.SpawnNewFood(); break;
                                case MapObject.Empty: this._Snake.MoveSnake(nextStep); break;
                                case MapObject.SnaleBody: IsAlive = false; this.Score = score; this.IsWin = false; break;
                                case MapObject.boardMapHorizontal: IsAlive = false; this.Score = score; this.IsWin = false; break;
                                case MapObject.boardMapVertical: IsAlive = false; this.Score = score; this.IsWin = false; break;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    this.Score = score;
                    this.IsWin = true;
                    return;
                }
                
            }
        }

        private void ChangeMap(Snake.SnakeSegment segment)
        {
            if (segment == null)
                return;
            else if (segment == this._Snake.GetHead())
                this._Map.SetCellInField(segment.GetPosition(), MapObject.SnakeHead);
            else
                this._Map.SetCellInField(segment.GetPosition(), MapObject.SnaleBody);
            this.ChangeMap(segment.GetNextSegment());
        }

        private void SpawnNewFood()
        {
            Point pointFood = this._Map.GetRandomFreeCell();
            this._Map.SetCellInField(pointFood, MapObject.Food);
            this._Map.FillCell(pointFood);
        }

        //public void GetKeyDown(KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.W: this._Snake.SetDirection(SnakeOrientation.UP); break;
        //        case Keys.S: this._Snake.SetDirection(SnakeOrientation.DOWN); break;
        //        case Keys.D: this._Snake.SetDirection(SnakeOrientation.RIGHT); break;
        //        case Keys.A: this._Snake.SetDirection(SnakeOrientation.LEFT); break;
        //    }
        //}
        public void GetKeyDown()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W: this._Snake.SetDirection(SnakeOrientation.UP); break;
                case ConsoleKey.S: this._Snake.SetDirection(SnakeOrientation.DOWN); break;
                case ConsoleKey.D: this._Snake.SetDirection(SnakeOrientation.RIGHT); break;
                case ConsoleKey.A: this._Snake.SetDirection(SnakeOrientation.LEFT); break;
            }
        }
    }
}
