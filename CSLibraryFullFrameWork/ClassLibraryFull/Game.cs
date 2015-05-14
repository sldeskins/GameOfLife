using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFull
{
    [Serializable]
    public class Game
    {
        private GameGrid _grid = null;

        private List<GridPosition> _gridTMinus1 = null;
        private List<GridPosition> _gridTMinus2 = null;
        private List<GridPosition> _gridTMinus0 = null;
        public List<GridPosition> AlivePositions
        {
            get
            {
                return _grid.getAliveCellPositions();
            }
            protected set
            {
                _gridTMinus2 = _gridTMinus1;
                _gridTMinus1 = _gridTMinus0;
                _gridTMinus0 = value;
            }
        }

        private int _currentGeneration = 0;
        public int CurrentGeneration
        {
            get
            {
                return _currentGeneration;
            }
            protected set
            {
                _currentGeneration = value;
            }
        }

        private int? _steadyStateGeneration = null;
        public int? SteadyStateGeneration
        {
            get
            {
                return _steadyStateGeneration;
            }
            protected set
            {
                _steadyStateGeneration = value;
            }
        }
        private GameStateEnum _gameState;

        public GameStateEnum GameState
        {
            get
            {
                return _gameState;
            }
            protected set
            {
                _gameState = value;
            }
        }

        public void StartNewGame ( int rows, int columns, List<GridPosition> initialLiveCells )
        {
            _grid = new GameGrid(rows, columns);
            _grid.setAliveCells(initialLiveCells);
            AlivePositions = initialLiveCells;
            GameState = GameStateEnum.NewGame;
            CurrentGeneration = 0;
            SteadyStateGeneration = null;
        }

        public GameGrid GetNextGeneration ()
        {
            GameGrid nextGeneration = _grid.getNextGeneration();
            AlivePositions = nextGeneration.getAliveCellPositions();
            calculateGameState();
            return nextGeneration;
        }

        private void calculateGameState ()
        {
            if (_gridTMinus0.Count == 0)
            {
                GameState = GameStateEnum.SteadyState_AllDead;
                SteadyStateGeneration = CurrentGeneration;
            }
            else if (_gridTMinus1 != null && _gridTMinus1 == _gridTMinus0)
            {
                GameState = GameStateEnum.SteadyState_StaticAlive;
                SteadyStateGeneration = CurrentGeneration;
            }
            else if (_gridTMinus2 != null && _gridTMinus2 == _gridTMinus0)
            {
                GameState = GameStateEnum.SteadyState_Blinker;
                SteadyStateGeneration = CurrentGeneration;
            }
            else
            {
                GameState = GameStateEnum.Continuing;
            }


        }



    }
}
