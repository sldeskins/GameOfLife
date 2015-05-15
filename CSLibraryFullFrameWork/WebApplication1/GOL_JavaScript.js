GOL = {};
//-- Game Engine Objects-------------------------
GOL.Game = {
}

GOL.GameStateEnum = {
    NewGame: { value: 0, name: "New Game" },
    SteadyState_AllDead: { value: 1, name: "Steady State: AllDead" },
    SteadyState_StaticAlive: { value: 2, name: "Steady State: Stable Alive" },
    SteadyState_Blinker: { value: 3, name: "Steady State: Blinker" }
};


GOL.GameFeaturesEnum = {
    Basic: { value: 0, name: "Basic" },
    ReplayGame: { value: 1, name: "Replay Game" },
    SaveGames: { value: 2, name: "Save Games" }
    //GameLibrary,
    //CellPictureStyling
};
GOL.GameGridPosition = {
    Rows: {},
    Columns: {}
};
GOL.GameCell = {
    _isAlive: { value: false },
    getIsAlive: function () {
        return _isAlive;
    },
    setIsAlive: function () {
        _isAlive = true;
    },
    getIsDead: function () {
        return !_isAlive;
    },
    setIsDead: function () {
        _isAlive = !false;
    }
}
GOL.GameGrid = {
    Rows: {},
    Columns: {},
    Cells: {},
    AliveCellPositions: {},
    GameGrid: function (rows, columns) {
        GOL.GameGrid.Rows = rows;
        GOL.GameGrid.Columns = columns;
        for (r = 0; r < rows; r++) {
            for (c = 0; c < columns; c++) {
                GOL.GameGrid.Cells[r, c] = new GOL.GameCell.setIsDead();
            }
        }
    },
    setAliveCell: function (row, column) {
        GOL.GameGrid.Cells[row, column] = new GOL.GameCell.setIsAlive();
    },
    setAliveCells: function (row, column, aliveCellPositions) {

        if (aliveCellPositions != null) {
            GOL.GameGrid.AliveCellPositions = aliveCellPositions;
            for (i = 0; i < length(aliveCellPositions) ; i++) {
                var alivePosition = aliveCellPositions[i];

                GOL.GameGrid.Cells[alivePosition.Row, aliveCellPositions.Column] = new GOL.GameCell.setIsAlive();
            }
        }
    },

    getCountLiveNeigborsForPosition: function (gridPosition) {
        var count = 0;
        //above left neighbor
        count += positionOnGridAndAlive(gridPosition.Row - 1, gridPosition.Column - 1);
        //above same neighbor
        count += positionOnGridAndAlive(gridPosition.Row, gridPosition.Column - 1);
        //above right neighbor
        count += positionOnGridAndAlive(gridPosition.Row + 1, gridPosition.Column - 1);

        // left neighbor
        count += positionOnGridAndAlive(gridPosition.Row - 1, gridPosition.Column);
        // selfdon't count
        //count += positionOnGridAndAlive(gridPosition.Row , gridPosition.Column  );
        // right neighbor 
        count += positionOnGridAndAlive(gridPosition.Row + 1, gridPosition.Column);

        //below left neighbor
        count += positionOnGridAndAlive(gridPosition.Row - 1, gridPosition.Column + 1);
        //below same neighbor
        count += positionOnGridAndAlive(gridPosition.Row, gridPosition.Column + 1);
        //below right neighbor
        count += positionOnGridAndAlive(gridPosition.Row + 1, gridPosition.Column + 1);


        return count;
    },
    positionOnGridAndAlive: function (row, column) {
        var result = 0;
        if (0 <= row && row < GOL.GameGrid.Rows &&
               0 <= column && column < GOL.GameGrid.Columns) {
            if (Cells[row, column].getIsAlive()) {
                result = 1;
            }
        }
        return result;
    },
    positionOnGridAndDeadCheck: function (gridPosition) {
        var result = false;
        if (0 <= gridPosition.Row && gridPosition.Row < GOL.GameGrid.Rows &&
            0 <= gridPosition.Column && gridPosition.Column < GOL.GameGrid.Columns) {
            result = Cells[gridPosition.Row, gridPosition.Column].getIsDead();
        }
        return result;
    },
    getNextGeneration: function () {
        var nextGenGrid = new GOL.GameGrid(GOL.GameGrid.Rows, GOL.GameGrid.Columns);
        var nextGenAlivePositions;
        GOL.checkDeadNeighborsOfLiveCells();


        //TODO var checkPositions = _deadCellsWithLiveNeighborPositions.Union(_aliveCellPositions).ToList();
        var checkPositions = null;
        for (i = 0; i < length(GOL.GameGrid._deadCellsWithLiveNeighborPositions) ; i++) {
            var pos = GOL.GameGrid.AliveCellPositions[i];
            checkPositions[pos.Row, pos.Column] = new GOL.GameCell.setIsDead();
        }
        for (i = 0; i < length(GOL.GameGrid.AliveCellPositions) ; i++) {
            var pos = GOL.GameGrid.AliveCellPositions[i];
            checkPositions[pos.Row, pos.Column] = new GOL.GameCell.setIsAlive();
        }

        for (i = 0; i < length(checkPositions) ; i++) {

            var _aliveCellPosition = checkPositions[i];

            var liveNeighbors = GOL.GameGrid.getCountLiveNeigborsForPosition(_aliveCellPosition);

            var cell = GOL.GameGrid.Cells[_aliveCellPosition.Row, _aliveCellPosition.Column];
            if (nextGenIsAlive(cell.getIsAlive(), liveNeighbors)) {
                nextGenAlivePositions[_aliveCellPosition.Row, _aliveCellPosition.Column](_aliveCellPosition);
            }
        }
        nextGenGrid.setAliveCells(nextGenAlivePositions);
        return nextGenGrid;

    },
    checkDeadNeighborsOfLiveCells: function () {

        for (i = 0; i < length(GOL.GameGrid.AliveCellPositions) ; i++) {

            var _aliveCellPosition = checkPositions[i];
            var neighborPosition = new GameGridPosition();

            //above left neighbor
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column - 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);
            //above same neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column - 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);
            //above right neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column - 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);

            // left neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);
            // selfdon't count
            //count += positionOnGridAndAlive(new GridPosition(_aliveCellPosition.Row , _aliveCellPosition.Column  ));
            // right neighbor  
            GOL.GameGrid.neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column);
            checkIfDeadAndAdd(neighborPosition);

            //below left neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row - 1, _aliveCellPosition.Column + 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);
            //below same neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row, _aliveCellPosition.Column + 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);
            //below right neighbor 
            neighborPosition = new GameGridPosition(_aliveCellPosition.Row + 1, _aliveCellPosition.Column + 1);
            GOL.GameGrid.checkIfDeadAndAdd(neighborPosition);

        }
    },
    checkIfDeadAndAdd: function (checkPosition) {
        if (GOL.GameGrid.positionOnGridAndDeadCheck(checkPosition)) {
            GOL.GameGrid._deadCellsWithLiveNeighborPositions[checkPosition.Row, checkPosition.Column] = new GOL.GameCell.setIsDead();
        }
    },
    nextGenIsAlive: function (isAlive, liveNeigbors) {
        if ((isAlive && (liveNeigbors == 2 || liveNeigbors == 3)) ||
                    (!isAlive && (liveNeigbors == 3))) {
            return true;
        }
        else {
            return false;
        }
    }

}
//---------------------------
GOL.UpdateGameGridRowsColumns = function () { };
GOL._makeNewGameAndLayoutGrid = function () { };
GOL.UpdateGameGridRowsColumns = function () {

    var button = document.getElementById("ButtonMakeGameGrid");
    if (_trySetRowsColumnsFromForm()) {

        button.Enabled = true;
    }
    else {
        button.Enabled = false;
    }
};
GOL.ResetGameAndLayoutGrid = function () {
    GOL._makeNewGameAndLayoutGrid();
    var panel = document.getElementById("PanelSaveGameArea");

    panel.style.visibility = "block";
};
GOL.ReplayGame() = function () {
    GOL._replayGame();
};
GOL.SaveGame() = function () {
    GOL._saveGame();
};
GOL.GetNextGeneration() = function () {
    GOL._getNextGeneration();
};
GOL.TimerID = null;
GOL.TimerInterval = 2000;
GOL.Timer = function (interval) {
    if (isNaN(interval)) {
        interval = GOL.TimerInterval;
    }
    setTimeout("_getNextGeneration()", interval);
}
GOL.UpdateTimerInterval = function () {
    var seconds;
    var textBox = document.getElementById("TextBoxTimer");
    var seconds = textBox.innerText;
    var interval = null;
    if (!isNaN(seconds)) {
        GOL.TimerInterval = 100 * seconds;
    }
    GOL.Timer();
};

GOL.TurnTimerOnOff = function () {
    GOL._turnTimerOnOff();
};
GOL._turnTimerOnOff = function () {

    if (GOL.TimerID != NaN) {
        clearTimeout(GOL.TimerID);
    }
    var checkBoxTimeOnOff = document.getElementById("CheckBoxTimeOnOff");
    if (checkBoxTimeOnOff.checked == 1) {
        GOL.TimerID = GOL.Timer();
    }
};

GOL._turnTimerOff = function () {
    if (GOL.TimerID != NaN) {
        clearTimeout(GOL.TimerID);
    }
    var checkBoxTimeOnOff = document.getElementById("CheckBoxTimeOnOff");
    checkBoxTimeOnOff.checked = 1;

};
GOL._replayGame = function () {
    GOL._makeNewGameAndLayoutGrid(false);
    foreach(pos in Game.AlivePositions)
    {
        var button = document.getElementById("pos.Row" + "_" + " pos.Column");
        _setButtonAliveStyle(button);
    }
};
GOL._makeNewGameAndLayoutGrid = function (formData) {
    if (formData == null) {
        formData = true;
    }
    GOL.AliveCellPositions = null;
    var r = 0;
    var c = 0;
    var haveValidRowsColumnsData = false;
    if (formData) {
        if (_trySetRowsColumnsFromForm()) {
            r = document.getElementById("TextBoxRows.Text").innerHTML;
            c = document.getElementById("TextBoxColumns.Text").innerHTML;
            haveValidRowsColumnsData = true;
        }
    }
    else {
        r = GOL.SavedGame.Rows;
        c = GOL.SavedGame.Columns;
        GOL.AliveCellPositions = GOL.SavedGame.InitialAlivePositions;
        haveValidRowsColumnsData = true;
    }

    //if (haveValidRowsColumnsData)
    //{
    //    Game game = new Game();
    //    game.StartNewGame(r, c, AliveCellPositions);
    //    Game = game;
    //    _layoutEmptyGameGrid();
    //    CheckBoxTimeOnOff.Checked = false;

    //    if (Game.GameFeatures.Contains(GameFeaturesEnum.ReplayGame) || Game.GameFeatures.Contains(GameFeaturesEnum.SaveGames))
    //    {
    //        SavedGame = new SavedGame();
    //        SavedGame.Rows = Game.GameBoard.Rows;
    //        SavedGame.Columns = Game.GameBoard.Columns;
    //        SavedGame.InitialAlivePositions = AliveCellPositions;
    //    }

    //}
};


