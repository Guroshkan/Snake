using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Map
    {
        public Map(int size)
        {
            if (size <= 0)
                throw new IncorrectValueExciption("Value less then zero");
            this._Size = size; //резервируем место для границ.
            this._FreeCellsCount = this._Size * this._Size;
            this._Field = new Cell[this._Size, this._Size];
            this._FreeCells = new List<Point>();
            for (int i = 0; i < this._Size; i++)
            {
                for (int j = 0; j < this._Size; j++)
                {
                    this._Field[i, j] = new Cell();
                    if (i != 0 || j != 0 || i != this._Size - 1 || j != this._Size - 1)
                        _FreeCells.Add(new Point(i, j));
                }
            }
            
        }

        private Cell[,] _Field;
        private int _Size;
        private int _FreeCellsCount;
        List<Point> _FreeCells;
        //разобраться с get set
        public int GetSize()
        {
            return this._Size;
        }
        public Cell GetCellFromField(Point point)
        {
            if (point._X < 0 || point._Y < 0 || point._X > this._Size || point._Y > this._Size)
                throw new IncorrectValueExciption($"Incorect coordinates x({point._X}) or y({point._Y})");
            return this._Field[point._X, point._Y];
        }

        public void SetCellInField(Point point, MapObject objectType)
        {
            //if (point._X < 0 || point._Y < 0 || point._X > this._Size-2 || point._Y > this._Size-2)
            //    throw new IncorrectValueExciption($"Incorect coordinates x({point._X}) or y({point._Y})");
            this._Field[point._X, point._Y] = new Cell(objectType);
        }

        public Cell[,] GetCopyFieldForOutput()
        {
            Cell[,] copy = new Cell[this._Size, this._Size];
            for (int i = 0; i < this._Size; i++)
            {
                for (int j = 0; j < this._Size; j++)
                {
                    copy[i, j] = this._Field[i, j];
                }
            }
            return copy;
        }
        public int GetCountFreeCells()
        {
            return this._FreeCellsCount;
        }
        public Point GetRandomFreeCell()
        {
            return this._FreeCells[new Random().Next(this._FreeCells.Count)];
        }
        public void FillCell(Point point)
        {
            this._FreeCells.Remove(point);
            this._FreeCellsCount--;
        }
    }
}