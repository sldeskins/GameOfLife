using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    [Serializable]
    public class GameGridPosition
    {
        private int _row;
        private int _column;
        public GameGridPosition ( int row, int column )
        {
            _row = row;
            _column = column;
        }
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }
        public override int GetHashCode ()
        {
            return _row ^ _column;
        }
        public override bool Equals ( object obj )
        {
            if (!(obj is GameGridPosition))
                return false;

            return Equals((GameGridPosition)obj);
        }

        public bool Equals ( GameGridPosition other )
        {
            if (_row != other._row)
                return false;

            return _column == other._column;
        }
        public static bool operator == ( GameGridPosition point1, GameGridPosition point2 )
        {
            return point1.Equals(point2);
        }

        public static bool operator != ( GameGridPosition point1, GameGridPosition point2 )
        {
            return !point1.Equals(point2);
        }
    }
}
