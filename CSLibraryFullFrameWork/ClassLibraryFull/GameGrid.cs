using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    [Serializable]
    public class GameGrid
    {
        private int _rows;
        private int _columns;

        private GameCell[,] _cells;

        private List<GameGridPosition> _aliveCellPositions = new List<GameGridPosition>();
        private List<GameGridPosition> _deadCellsWithLiveNeighborPositions = new List<GameGridPosition>();


        public GameGrid ( int rows, int columns )
        {
            _rows = rows;
            _columns = columns;
            _cells = new GameCell[rows, columns];
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    _cells[r, c] = new GameCell(false);
                }
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
        }
        public int Columns
        {
            get
            {
                return _columns;

            }
        }
        public GameCell getCell ( int row, int column )
        {
            return _cells[row, column];
        }
        public GameCell[,] getCells ()
        {
            return _cells;
        }
        public void setAliveCell ( int row, int column )
        {
            _cells[row, column].IsAlive = true;
            if (!_aliveCellPositions.Contains(new GameGridPosition(row, column)))
            {
                _aliveCellPositions.Add(new GameGridPosition(row, column));
            }
        }
        public void setDead ( int row, int column )
        {
            _cells[row, column].IsDead = true;
            if (_aliveCellPositions.Contains(new GameGridPosition(row, column)))
            {
                _aliveCellPositions.Remove(new GameGridPosition(row, column));
            }
        }

        public void setAliveCells ( List<GameGridPosition> aliveCellPositions )
        {
            if (aliveCellPositions != null)
            {
                foreach (var aliveCellPosition in aliveCellPositions)
                {
                    _cells[aliveCellPosition.Row, aliveCellPosition.Column].IsAlive = true;
                    if (!_aliveCellPositions.Contains(aliveCellPosition))
                    {
                        _aliveCellPositions.Add(aliveCellPosition);
                    }
                }
            }
        }

        public List<GameGridPosition> getAliveCellPositions ()
        {
            return _aliveCellPositions;
        }

        public int getCountLiveNeigborsForPosition ( GameGridPosition gridPosition )
        {
            int count = 0;
            //above left neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row - 1, gridPosition.Column - 1));
            //above same neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row, gridPosition.Column - 1));
            //above right neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row + 1, gridPosition.Column - 1));

            // left neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row - 1, gridPosition.Column));
            // selfdon't count
            //count += positionOnGridAndAlive(new GridPosition(gridPosition.Row , gridPosition.Column  ));
            // right neighbor 
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row + 1, gridPosition.Column));

            //below left neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row - 1, gridPosition.Column + 1));
            //below same neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row, gridPosition.Column + 1));
            //below right neighbor
            count += positionOnGridAndAlive(new GameGridPosition(gridPosition.Row + 1, gridPosition.Column + 1));

            return count;
        }
        private int positionOnGridAndAlive ( GameGridPosition gridPosition )
        {
            int result = 0;
            if (0 <= gridPosition.Row && gridPosition.Row < _rows &&
                0 <= gridPosition.Column && gridPosition.Column < _columns)
            {
                if (getCell(gridPosition.Row, gridPosition.Column).IsAlive)
                {
                    result = 1;
                }
            }
            return result;
        }
        private bool positionOnGridAndDeadCheck ( GameGridPosition gridPosition )
        {
            bool result = false;
            if (0 <= gridPosition.Row && gridPosition.Row < _rows &&
                0 <= gridPosition.Column && gridPosition.Column < _columns)
            {
                result = getCell(gridPosition.Row, gridPosition.Column).IsDead;
            }
            return result;
        }
        public GameGrid getNextGeneration ()
        {
            GameGrid nextGenGrid = new GameGrid(_rows, _columns);
            List<GameGridPosition> nextGenAlivePositions = new List<GameGridPosition>();

            checkDeadNeighborsOfLiveCells();
            var checkPositions = _deadCellsWithLiveNeighborPositions.Union(_aliveCellPositions).ToList();

            foreach (var _aliveCellPosition in checkPositions)
            {
                int liveNeighbors = getCountLiveNeigborsForPosition(_aliveCellPosition);
                var cell = _cells[_aliveCellPosition.Row, _aliveCellPosition.Column];
                if (!nextGenAlivePositions.Contains(_aliveCellPosition) && nextGenIsAlive(cell.IsAlive, liveNeighbors))
                {
                    nextGenAlivePositions.Add(_aliveCellPosition);
                }
            }



            nextGenGrid.setAliveCells(nextGenAlivePositions);
            return nextGenGrid;
        }
        private void checkDeadNeighborsOfLiveCells ()
        {
            foreach (var _aliveCellPosition in _aliveCellPositions)
            {
                GameGridPosition neighborPosition;

                //above left neighbor
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);
                //above same neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);
                //above right neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);

                // left neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column);
                checkIfDeadAndAdd(neighborPosition);
                // selfdon't count
                //count += positionOnGridAndAlive(new GridPosition(_aliveCellPosition.Row , _aliveCellPosition.Column  ));
                // right neighbor  
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column);
                checkIfDeadAndAdd(neighborPosition);

                //below left neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);
                //below same neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);
                //below right neighbor 
                neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);

            }
        }

        private void checkIfDeadAndAdd ( GameGridPosition checkPosition )
        {
            if (positionOnGridAndDeadCheck(checkPosition) && !_deadCellsWithLiveNeighborPositions.Contains(checkPosition))
            {
                _deadCellsWithLiveNeighborPositions.Add(checkPosition);
            }
        }
        private bool nextGenIsAlive ( bool isAlive, int liveNeigbors )
        {
            if ((isAlive && (liveNeigbors == 2 || liveNeigbors == 3)) ||
                (!isAlive && (liveNeigbors == 3)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
