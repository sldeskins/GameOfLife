using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryFull;
using System.Collections.Generic;

namespace ClassLibraryFullTest
{
    [TestClass]
    public class GameDescriptionTest
    {
        /*
         * Your task is to write a program to calculate the next
generation of Conway's game of life, given any starting
position. You start with a two dimensional grid of cells, 
where each cell is either alive or dead. The grid is finite, 
and no life can exist off the edges. When calculating the 
next generation of the grid, follow these four rules:

1. Any live cell with fewer than two live neighbours dies, 
   as if caused by underpopulation.
2. Any live cell with more than three live neighbours dies, 
   as if by overcrowding.
3. Any live cell with two or three live neighbours lives 
   on to the next generation.
4. Any dead cell with exactly three live neighbours becomes 
   a live cell.

Examples: * indicates live cell, . indicates dead cell

Example input: (4 x 8 grid)
4 8
........
....*...
...**...
........

Example output:
4 8
........
...**...
...**...
........

         */
        [TestMethod]
        public void YouStartWithATwoDimensionalGrid ()
        {
            GameGrid grid = new GameGrid(7, 8);
            Assert.AreEqual(7, grid.Rows);
            Assert.AreEqual(8, grid.Columns);
        }
        [TestMethod]
        public void CellIsEitherAliveOrDead_Alive ()
        {
            Cell cell = new Cell(true);
            Assert.AreEqual(true, cell.IsAlive);
            Assert.AreEqual(false, cell.IsDead);
        }
        [TestMethod]
        public void CellIsEitherAliveOrDead_Dead ()
        {
            Cell cell = new Cell(false);
            Assert.AreEqual(false, cell.IsAlive);
            Assert.AreEqual(true, cell.IsDead);
        }

        [TestMethod]
        public void CellIsEitherAliveOrDead ()
        {
            Cell cell = new Cell();
            Assert.AreEqual(false, cell.IsAlive);
            Assert.AreEqual(true, cell.IsDead);
        }

        [TestMethod]
        public void _getGridCells ()
        {
            GameGrid grid = new GameGrid(7, 8);
            Cell[,] gridCells = grid.getCells();
            Assert.AreEqual(56, gridCells.Length);
            Assert.IsInstanceOfType(gridCells[1, 1], typeof(Cell));
            Assert.AreEqual(false, gridCells[1, 1].IsAlive);
            Assert.AreEqual(true, gridCells[1, 1].IsDead);
        }
        [TestMethod]
        public void _setGridCell_Alive ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);

            int row = 2;
            int column = 3;
            grid.setAliveCell(row, column);
        }
        [TestMethod]
        public void _setGridCell_Alive_ThenDead ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);

            int row = 2;
            int column = 3;

            //set alive
            grid.setAliveCell(row, column);

            Cell[,] gridCells = grid.getCells();
            Assert.AreEqual(true, gridCells[row, column].IsAlive);
            Assert.AreEqual(false, gridCells[row + 1, column + 1].IsAlive);
            Assert.AreEqual(false, gridCells[row - 1, column - 1].IsAlive);

            //set dead
            grid.setDead(row, column);

            gridCells = grid.getCells();
            Assert.AreEqual(false, gridCells[row, column].IsAlive);
            Assert.AreEqual(false, gridCells[row + 1, column + 1].IsAlive);
            Assert.AreEqual(false, gridCells[row - 1, column - 1].IsAlive);
        }

        [TestMethod]
        public void _setGridCell_setAliveCells ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);

            //setup
            int row1 = 1;
            int col1 = 3;
            //
            int row2 = 6;
            int col2 = 5;

            //before
            var cells = grid.getCells();
            Assert.AreEqual(false, cells[row1, col1].IsAlive);
            Assert.AreEqual(false, cells[row2, col2].IsAlive);

            //
            List<GridPosition> cellsAreAlive = new List<GridPosition>() { new GridPosition(row1, col1), new GridPosition(row2, col2) };
            grid.setAliveCells(cellsAreAlive);

            //after
            cells = grid.getCells();
            Assert.AreEqual(true, cells[row1, col1].IsAlive);
            Assert.AreEqual(true, cells[row2, col2].IsAlive);
        }
        [TestMethod]
        public void _setGridCell_setAliveCells_null ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);
           
            grid.setAliveCells(null);
            var results = grid.getAliveCellPositions();
            Assert.AreEqual(0, results.Count);
            Assert.IsInstanceOfType(results, typeof(List<GridPosition>));
        }
        [TestMethod]
        public void _setGridCell_GetAliveCells_none ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);
            List<GridPosition> cellsAreAlive = grid.getAliveCellPositions();
            Assert.AreEqual(0, cellsAreAlive.Count);

        }
        [TestMethod]
        public void _setGridCell_getAliveCellPositions ()
        {
            int rows = 2;
            int columns = 4;
            GameGrid grid = new GameGrid(rows, columns);

            //setup
            int row1 = 1;
            int col1 = 3;
            //
            int row2 = 0;
            int col2 = 0;
            //
            int row3 = 1;
            int col3 = 1;

            //before
            var cells = grid.getCells();
            Assert.AreEqual(false, cells[row1, col1].IsAlive);
            Assert.AreEqual(false, cells[row2, col2].IsAlive);
            Assert.AreEqual(false, cells[row3, col3].IsAlive);
            //
            List<GridPosition> cellsAreAlive = new List<GridPosition>() { 
                new GridPosition(row1, col1), 
                new GridPosition(row2, col2), 
                new GridPosition(row3, col3) };

            grid.setAliveCells(cellsAreAlive);

            //after
            var positions = grid.getAliveCellPositions();
            Assert.AreEqual(3, positions.Count);
            Assert.AreEqual(true, grid.getCell(row1, col1).IsAlive);
            Assert.AreEqual(true, grid.getCell(row2, col2).IsAlive);
            Assert.AreEqual(true, grid.getCell(row3, col3).IsAlive);
        }

        [TestMethod]
        public void GridOfCellsWhereEachCellIsEitherAliveOrDead ()
        {
            int rows = 2;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);

            //
            //setup
            int row1 = 1;
            int col1 = 2;
            //
            int row2 = 0;
            int col2 = 0;
            //
            int row3 = 1;
            int col3 = 1;

            grid.setAliveCells(new List<GridPosition>() { 
                new GridPosition(row1, col1), 
                new GridPosition(row2, col2), 
                new GridPosition(row3, col3) });

            //
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if ((r == row1 && c == col1) || (r == row2 && c == col2) || (r == row3 && c == col3))
                    {
                        Assert.AreEqual(true, grid.getCell(r, c).IsAlive);
                        Assert.AreEqual(false, grid.getCell(r, c).IsDead);
                    }
                    else
                    {
                        Assert.AreEqual(false, grid.getCell(r, c).IsAlive);
                        Assert.AreEqual(true, grid.getCell(r, c).IsDead);
                    }
                }
            }

        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_OffGrid ()
        {
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            //
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(-1, -1));
            Assert.AreEqual(0, count);
            //
            count = grid.getCountLiveNeigborsForPosition(new GridPosition(-8, 8));
            Assert.AreEqual(0, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_EdgeGrid ()
        {
            //_00
            //000
            //000
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(0, 0));
            Assert.AreEqual(0, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_EdgeGrid_1liveNeighbor ()
        {
            //_10
            //000
            //000
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 1);

            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(0, 0));
            Assert.AreEqual(1, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_EdgeGrid_1liveNeighbor_shouldNotCountSelf ()
        {
            //A10
            //000
            //000
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 1);
            grid.setAliveCell(0, 0);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(0, 0));
            Assert.AreEqual(1, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_EdgeGrid_2liveNeighbor_edge ()
        {
            //100
            //011
            //10_
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 0);
            grid.setAliveCell(1, 1);
            grid.setAliveCell(1, 2);
            grid.setAliveCell(1, 1);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(2, 2));
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_EdgeGrid_3liveNeighbr ()
        {
            //111
            //111
            //11_
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 0);
            grid.setAliveCell(0, 1);
            grid.setAliveCell(0, 2);
            grid.setAliveCell(1, 0);
            grid.setAliveCell(1, 1);
            grid.setAliveCell(1, 2);
            grid.setAliveCell(2, 0);
            grid.setAliveCell(2, 1);
            grid.setAliveCell(2, 2);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(2, 2));
            Assert.AreEqual(3, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_within_8liveNeighbors ()
        {
            //111
            //1_1
            //111
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 0);
            grid.setAliveCell(0, 1);
            grid.setAliveCell(0, 2);
            grid.setAliveCell(1, 0);
            grid.setAliveCell(1, 1);
            grid.setAliveCell(1, 2);
            grid.setAliveCell(2, 0);
            grid.setAliveCell(2, 1);
            grid.setAliveCell(2, 2);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(1, 1));
            Assert.AreEqual(8, count);
        }
        [TestMethod]
        public void GridGetNumberOfLiveNeighbors_within_5liveNeighbors ()
        {
            //101
            //0_1
            //101
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);
            grid.setAliveCell(0, 0);
            //grid.setAliveCell(0, 1);
            grid.setAliveCell(0, 2);
            //grid.setAliveCell(1, 0);
            grid.setAliveCell(1, 1);
            grid.setAliveCell(1, 2);
            grid.setAliveCell(2, 0);
            //grid.setAliveCell(2, 1);
            grid.setAliveCell(2, 2);
            int count = grid.getCountLiveNeigborsForPosition(new GridPosition(1, 1));
            Assert.AreEqual(5, count);
        }

        /*
Cell next gen rules
         */
        [TestMethod]
        //1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
        public void NextGenerationOfGrid_1 ()
        {
            //000
            //000
            //010
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);

            //grid.setAliveCell(0, 0);
            //grid.setAliveCell(0, 1);
            // grid.setAliveCell(0, 2);
            //grid.setAliveCell(1, 0);
            //grid.setAliveCell(1, 1);
            //grid.setAliveCell(1, 2);
            // grid.setAliveCell(2, 0);
            grid.setAliveCell(2, 1);
            //grid.setAliveCell(2, 2);

            GameGrid nextGenerationGrid = grid.getNextGeneration();
            Assert.AreEqual(grid.Rows, nextGenerationGrid.Rows);
            Assert.AreEqual(grid.Columns, nextGenerationGrid.Columns);

            var nextGetAlivePositions = nextGenerationGrid.getAliveCellPositions();
            Assert.AreEqual(0, nextGetAlivePositions.Count);
            //Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(1, 1)));

        }

        [TestMethod]
        //4. Any dead cell with exactly three live neighbours becomes a live cell.
        public void NextGenerationOfGrid_4 ()
        {
            //010
            //110
            //000

            //nextgen
            //110
            //110
            //000
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);

            //grid.setAliveCell(0, 0);
            grid.setAliveCell(0, 1);
            //grid.setAliveCell(0, 2);
            grid.setAliveCell(1, 0);
            grid.setAliveCell(1, 1);
            //grid.setAliveCell(1, 2);
            // grid.setAliveCell(2, 0);
            // grid.setAliveCell(2, 1);
            // grid.setAliveCell(2, 2);

            GameGrid nextGenerationGrid = grid.getNextGeneration();
            Assert.AreEqual(grid.Rows, nextGenerationGrid.Rows);
            Assert.AreEqual(grid.Columns, nextGenerationGrid.Columns);

            var nextGetAlivePositions = nextGenerationGrid.getAliveCellPositions();
            Assert.AreEqual(4, nextGetAlivePositions.Count);

            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(0, 0)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(0, 1)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(1, 0)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(1, 1)));

        }
        [TestMethod]
        //3. Any live cell with two or three live neighbours lives on to the next generation.
        //2. Any live cell with more than three live neighbours dies, as if by overcrowding.
        public void NextGenerationOfGrid_3 ()
        {
            //101
            //010
            //011

            //nextgen
            //010
            //100
            //011
            int rows = 3;
            int columns = 3;
            GameGrid grid = new GameGrid(rows, columns);

            grid.setAliveCell(0, 0);
            //grid.setAliveCell(0, 1);
            grid.setAliveCell(0, 2);
            //grid.setAliveCell(1, 0);
            grid.setAliveCell(1, 1);
            //grid.setAliveCell(1, 2);
            // grid.setAliveCell(2, 0);
            grid.setAliveCell(2, 1);
            grid.setAliveCell(2, 2);
            //
            Assert.AreEqual(1, grid.getCountLiveNeigborsForPosition(new GridPosition(0, 0)));
            Assert.AreEqual(3, grid.getCountLiveNeigborsForPosition(new GridPosition(0, 1)));
            Assert.AreEqual(1, grid.getCountLiveNeigborsForPosition(new GridPosition(0, 2)));
            Assert.AreEqual(3, grid.getCountLiveNeigborsForPosition(new GridPosition(1, 0)));
            Assert.AreEqual(4, grid.getCountLiveNeigborsForPosition(new GridPosition(1, 1)));
            Assert.AreEqual(4, grid.getCountLiveNeigborsForPosition(new GridPosition(1, 2)));
            Assert.AreEqual(2, grid.getCountLiveNeigborsForPosition(new GridPosition(2, 0)));
            Assert.AreEqual(2, grid.getCountLiveNeigborsForPosition(new GridPosition(2, 1)));
            Assert.AreEqual(2, grid.getCountLiveNeigborsForPosition(new GridPosition(2, 2)));
            //
            GameGrid nextGenerationGrid = grid.getNextGeneration();
            Assert.AreEqual(grid.Rows, nextGenerationGrid.Rows);
            Assert.AreEqual(grid.Columns, nextGenerationGrid.Columns);

            var nextGetAlivePositions = nextGenerationGrid.getAliveCellPositions();
            Assert.AreEqual(4, nextGetAlivePositions.Count);

            Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(0, 0)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(0, 1)));
            Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(0, 2)));

            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(1, 0)));
            Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(1, 1)));
            Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(1, 2)));

            Assert.IsFalse(nextGetAlivePositions.Contains(new GridPosition(2, 0)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(2, 1)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(2, 2)));

        }


        [TestMethod]
        public void NextGenerationOfGrid_4x8Example ()
        {
            /*
         
             Examples: * indicates live cell, . indicates dead cell

    Example input: (4 x 8 grid)
    4 8
    ........
    ....*...
    ...**...
    ........

    Example output:
    4 8
    ........
    ...**...
    ...**...
    ........

             */
            int rows = 4;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);

            grid.setAliveCell(1, 4);
            grid.setAliveCell(2, 3);
            grid.setAliveCell(2, 4);

            GameGrid nextGenerationGrid = grid.getNextGeneration();
            Assert.AreEqual(grid.Rows, nextGenerationGrid.Rows);
            Assert.AreEqual(grid.Columns, nextGenerationGrid.Columns);

            var nextGetAlivePositions = nextGenerationGrid.getAliveCellPositions();
            Assert.AreEqual(4, nextGetAlivePositions.Count);

            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(1, 4)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(2, 3)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(2, 4)));
            Assert.IsTrue(nextGetAlivePositions.Contains(new GridPosition(1, 3)));
        }
        [TestMethod]
        public void _setGridCell_AliveTwiceDoesNotChangeTheNumberOfLiveCells ()
        {
            int rows = 7;
            int columns = 8;
            GameGrid grid = new GameGrid(rows, columns);

            int row = 2;
            int column = 3;
            //
            grid.setAliveCell(row, column);
            List<GridPosition> results = grid.getAliveCellPositions();
            Assert.AreEqual(1, results.Count);
            //
            grid.setAliveCell(row, column);
            results = grid.getAliveCellPositions();
            Assert.AreEqual(1, results.Count);
        }
    }

}

