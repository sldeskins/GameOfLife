using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    [Serializable]
    public class Game
    {
        private GameGrid _grid = new GameGrid(0, 0);
        public GameGrid GameBoard
        {
            get
            {
                return _grid;
            }
        }

        private List<GameGridPosition> _gridTMinus1 = null;
        private List<GameGridPosition> _gridTMinus2 = null;
        private List<GameGridPosition> _gridTMinus0 = null;
        public List<GameGridPosition> AlivePositions
        {
            get
            {
                return _grid.getAliveCellPositions();
            }
            set
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

        public void StartNewGame ( int rows, int columns, List<GameGridPosition> initialLiveCells )
        {
            _grid = new GameGrid(rows, columns);
            _grid.setAliveCells(initialLiveCells);
            AlivePositions = initialLiveCells;
            GameState = GameStateEnum.NewGame;
            CurrentGeneration = 0;
            SteadyStateGeneration = null;
        }
        public void StartNewGame ( int rows, int columns )
        {
            StartNewGame(rows, columns, AlivePositions);
        }
        public GameGrid GetNextGeneration ()
        {
            CurrentGeneration++;
            GameGrid nextGeneration = _grid.getNextGeneration();
            AlivePositions = nextGeneration.getAliveCellPositions();
            _grid = nextGeneration;
            calculateGameState();
            return _grid;
        }

        private void calculateGameState ()
        {

            if (_gridTMinus0.Count == 0)
            {
                GameState = GameStateEnum.SteadyState_AllDead;
                setSteadyStateGeneration();
            }
            else if (_gridTMinus1 != null && allPointsMatch(_gridTMinus1, _gridTMinus0))
            {
                GameState = GameStateEnum.SteadyState_StaticAlive;
                setSteadyStateGeneration();
            }
            else if (_gridTMinus2 != null && allPointsMatch(_gridTMinus2, _gridTMinus0))
            {
                GameState = GameStateEnum.SteadyState_Blinker;
                setSteadyStateGeneration();
            }
            else
            {
                GameState = GameStateEnum.Continuing;
            }

        }
        private void setSteadyStateGeneration ()
        {
            if (SteadyStateGeneration == null)
            {
                SteadyStateGeneration = CurrentGeneration;
            }
        }
        private bool allPointsMatch ( List<GameGridPosition> gridPositions1, List<GameGridPosition> gridPositions2 )
        {
            if (gridPositions1 == null && gridPositions2 != null)
                return false;
            if (gridPositions1 != null && gridPositions2 != null && gridPositions1.Count != gridPositions2.Count)
                return false;
            for (int i = 0; i < gridPositions1.Count; i++)
            {
                if (gridPositions1[i] != gridPositions2[i])
                {
                    return false;
                }
            }
            return true;
        }

        #region SavedGame components
        private List<SavedGame> _saveGames = new List<SavedGame>();
        public List<SavedGame> SavedGames
        {
            get
            {
                return _saveGames;
            }
            set
            {
                _saveGames = value;
            }
        }
        #endregion SavedGame components

    }
}
