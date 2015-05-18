using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    [Serializable]
    public class SavedGame
    {
        private DateTime _dateTimeSaved;
        public DateTime SavedDatedTime
        {
            get
            {
                return _dateTimeSaved;
            }
            set
            {
                _dateTimeSaved = value;
            }
        }
        
        private List<GameGridPosition> _initalAlivePosistions;
        public List<GameGridPosition> InitialAlivePositions
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
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
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
