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

        private Cell[,] _cells;

        private List<GridPosition> _aliveCellPositions = new List<GridPosition>();
        private List<GridPosition> _deadCellsWithLiveNeighborPositions = new List<GridPosition>();


        public GameGrid ( int rows, int columns )
        {
            _rows = rows;
            _columns = columns;
            _cells = new Cell[rows, columns];
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    _cells[r, c] = new Cell(false);
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
        public Cell getCell ( int row, int column )
        {
            return _cells[row, column];
        }
        public Cell[,] getCells ()
        {
            return _cells;
        }
        public void setAliveCell ( int row, int column )
        {
            _cells[row, column].IsAlive = true;
            if (!_aliveCellPositions.Contains(new GridPosition(row, column)))
            {
                _aliveCellPositions.Add(new GridPosition(row, column));
            }
        }
        public void setDead ( int row, int column )
        {
            _cells[row, column].IsDead = true;
            if (_aliveCellPositions.Contains(new GridPosition(row, column)))
            {
                _aliveCellPositions.Remove(new GridPosition(row, column));
            }
        }

        public void setAliveCells ( List<GridPosition> aliveCellPositions )
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

        public List<GridPosition> getAliveCellPositions ()
        {
            return _aliveCellPositions;
        }

        public int getCountLiveNeigborsForPosition ( GridPosition gridPosition )
        {
            int count = 0;
            //above left neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row - 1, gridPosition.Column - 1));
            //above same neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row, gridPosition.Column - 1));
            //above right neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row + 1, gridPosition.Column - 1));

            // left neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row - 1, gridPosition.Column));
            // selfdon't count
            //count += positionOnGridAndAlive(new GridPosition(gridPosition.Row , gridPosition.Column  ));
            // right neighbor 
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row + 1, gridPosition.Column));

            //below left neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row - 1, gridPosition.Column + 1));
            //below same neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row, gridPosition.Column + 1));
            //below right neighbor
            count += positionOnGridAndAlive(new GridPosition(gridPosition.Row + 1, gridPosition.Column + 1));

            return count;
        }
        private int positionOnGridAndAlive ( GridPosition gridPosition )
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
        private bool positionOnGridAndDeadCheck ( GridPosition gridPosition )
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
            List<GridPosition> nextGenAlivePositions = new List<GridPosition>();

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
                GridPosition neighborPosition;

                //above left neighbor
                neighborPosition = new GridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);
                //above same neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);
                //above right neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column - 1);
                checkIfDeadAndAdd(neighborPosition);

                // left neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column);
                checkIfDeadAndAdd(neighborPosition);
                // selfdon't count
                //count += positionOnGridAndAlive(new GridPosition(_aliveCellPosition.Row , _aliveCellPosition.Column  ));
                // right neighbor  
                neighborPosition = new GridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column);
                checkIfDeadAndAdd(neighborPosition);

                //below left neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);
                //below same neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);
                //below right neighbor 
                neighborPosition = new GridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column + 1);
                checkIfDeadAndAdd(neighborPosition);

            }
        }

        private void checkIfDeadAndAdd ( GridPosition checkPosition )
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
