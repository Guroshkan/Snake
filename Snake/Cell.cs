using System;
using System.Collections.Generic;
using System.Text;
enum MapObject {Empty = ' ', Food = '$', SnakeHead = '*', SnaleBody = '0', wall = '▊',boardMapHorizontal = '━',boardMapVertical = '┃',
    boardMapAngleTopRight = '┐', boardMapAngleTopLeft = '┌', boardMapAngleBotRight = '┘', boardMapAngleBotLeft = '└'
}

namespace Snake
{
    class Cell
    {
        public Cell()
        {
            this.Value = MapObject.Empty;
        }

        public Cell(MapObject objectType)
        {
            this.Value = objectType;
        }
        public MapObject Value { get; }
    }
}
