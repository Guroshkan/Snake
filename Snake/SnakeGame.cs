
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
        const int TIMESLEEP = 50;
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
        private Point _FoodCoord;

        Thread ReadKeyThread = null;

        public void StartGame()
        {
            this.SpawnNewFood();
            ReadKeyThread = new Thread(GetKeyDown);
            ReadKeyThread.Start();
            LoopGame();
        }

        private void LoopGame()
        {
            int score = 0;
            bool IsAlive = true;
            while (IsAlive)
            {
                //this.GetKeyDown();
                Point nextStep = new Point(this._Snake.GetHead().GetPosition()._X, this._Snake.GetHead().GetPosition()._Y);
                switch (this._Snake.GetDirection())
                {
                    case SnakeOrientation.UP: nextStep = new Point(nextStep._X, nextStep._Y + 1); break;
                    case SnakeOrientation.DOWN: nextStep = new Point(nextStep._X, nextStep._Y - 1); break;
                    case SnakeOrientation.LEFT: nextStep = new Point(nextStep._X - 1, nextStep._Y); break;
                    case SnakeOrientation.RIGHT: nextStep = new Point(nextStep._X + 1, nextStep._Y); break;
                }

                this.CycleMap(nextStep);


                this.ClearMap();
                this.ChangeMap(this._Snake.GetHead());
                Output.Show(this._Map);
                Thread.Sleep(TIMESLEEP);

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

        private void ClearMap()
        {
            for (int i = 0; i < this._Map.GetSize(); i++)
            {
                for (int j = 0; j < this._Map.GetSize(); j++)
                {
                    this._Map.SetCellInField(new Point(i, j), MapObject.Empty);
                }
            }
        }

        private void ChangeMap(Snake.SnakeSegment segment)
        {
            if (segment == null)
            {
                this._Map.SetCellInField(this._FoodCoord, MapObject.Food);//потенциально нужно сохранять еще и стены
                return;
            }
                
            else if (segment == this._Snake.GetHead())
                this._Map.SetCellInField(segment.GetPosition(), MapObject.SnakeHead);
            else
                this._Map.SetCellInField(segment.GetPosition(), MapObject.SnaleBody);
            this.ChangeMap(segment.GetNextSegment());
        }

        private void SpawnNewFood()
        {
            Point pointFood = this._Map.GetRandomFreeCell();//новая еда может совпадать с сегментом змеи
            this._Map.SetCellInField(pointFood, MapObject.Food);
            this._Map.FillCell(pointFood);
            this._FoodCoord = pointFood;
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
            while (ReadKeyThread.IsAlive)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D: this._Snake.SetDirection(SnakeOrientation.UP); break;
                    case ConsoleKey.A: this._Snake.SetDirection(SnakeOrientation.DOWN); break;
                    case ConsoleKey.S: this._Snake.SetDirection(SnakeOrientation.RIGHT); break;
                    case ConsoleKey.W: this._Snake.SetDirection(SnakeOrientation.LEFT); break;
                }
            }
            
        }

        private void CycleMap(Point nextStep)
        {
            if (nextStep._X < 0)
                nextStep._X = this._Map.GetSize() - 1;
            if (nextStep._Y < 0)
                nextStep._Y = this._Map.GetSize() - 1;
            if (nextStep._X >= this._Map.GetSize())
                nextStep._X = 0;
            if (nextStep._Y >= this._Map.GetSize())
                nextStep._Y = 0;
        }
    }
}
