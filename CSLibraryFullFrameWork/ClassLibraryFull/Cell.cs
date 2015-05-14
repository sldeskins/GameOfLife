using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFull
{
    [Serializable]
    public class Cell
    {
        private bool _isAlive;
        public Cell ( bool isAlive = false )
        {
            _isAlive = isAlive;
        }
        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }
            set
            {
                _isAlive = value;
            }
        }
        public bool IsDead
        {
            get
            {
                return !_isAlive;
            }
            set
            {
                _isAlive = !value;
            }
        }
    }
}
