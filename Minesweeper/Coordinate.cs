using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }

        public int Y { get; }

        public override bool Equals(object? obj)
        {
            var other = obj as Coordinate;

            if(X == other.X && Y == other.Y)
            {
                return true;
            }

            return false;
        }
    }
}
