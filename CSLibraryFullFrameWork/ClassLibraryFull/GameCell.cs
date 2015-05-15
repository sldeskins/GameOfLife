using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    [Serializable]
    public class GameCell
    {
        private bool _isAlive;
        public GameCell ( bool isAlive = false )
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
