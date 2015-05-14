using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFull
{
    [Serializable]
    public class SavedGame
    {
        private List<GridPosition> _initalAlivePosistions;

        public List<GridPosition> InitalAlivePositions
        {
            get
            {
                return _initalAlivePosistions;
            }
            set
            {
                _initalAlivePosistions = value;
            }
        }

        private int _gridRows;

        public int Rows
        {
            get
            {
                return _gridRows;
            }
            set
            {
                _gridRows = value;
            }
        }
        private int _gridColumns;

        public int Columns
        {
            get
            {
                return _gridColumns;
            }
            set
            {
                _gridColumns = value;
            }
        }
        private int _endGeneration;

        public int EndGeneration
        {
            get
            {
                return _endGeneration;
            }
            set
            {
                _endGeneration = value;
            }
        }
        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        private GameStateEnum _gameState;

        public GameStateEnum EndState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }


    }
}
