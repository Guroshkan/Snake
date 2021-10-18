using System;
using System.Collections.Generic;
using System.Text;
public enum SnakeOrientation { UP = 0, RIGHT, DOWN, LEFT } 

namespace Snake
{
    public class Snake
    {
        public class SnakeSegment
        {
            Point _Point;
            private SnakeSegment _NextSegment;
            public SnakeSegment(Point point)
            {
                this._Point = point;
            } 
            public Point GetPosition()
            {
                return _Point;
            }
            public SnakeSegment GetNextSegment()
            {
                return _NextSegment;
            }
            public void SetNextSegment(SnakeSegment snakeSegment)
            {
                this._NextSegment = snakeSegment;
            }
            public void MoveSegment(Point point)
            {
                Point currentPosition = this._Point;
                this._Point = point;
                if (this._NextSegment != null)
                    this._NextSegment.MoveSegment(currentPosition);
            }
        }

        public Snake(Point point)
        {
            this._Head = new SnakeSegment(point);
        }

        private SnakeSegment _Head;
        private SnakeOrientation _Direction;

        public void AddSegment(Point point)
        {
            SnakeSegment NewHead = new SnakeSegment(point);
            NewHead.SetNextSegment(this._Head);
            this._Head = NewHead;
        }
        public SnakeSegment GetHead()
        {
            return this._Head;
        }
        public SnakeOrientation GetDirection()
        {
            return this._Direction;
        }
        public void SetDirection(SnakeOrientation direction)
        {
            this._Direction = direction;
        }
        public void MoveSnake(Point point)
        {
            this._Head.MoveSegment(point);
        }
    }
}
