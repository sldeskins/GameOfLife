using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CS_GOL_LibraryFull;

namespace WebApplication1
{
    public partial class GameUI : System.Web.UI.Page
    {
        #region persistance
        private GameGrid _grid;

        protected Game Game
        {
            get
            {
                return (Game)ViewState["Game"];
            }
            set
            {
                ViewState["Game"] = value;
            }
        }
        protected List<GridPosition> AliveCellPositions
        {
            get
            {
                return (List<GridPosition>)ViewState["AliveCellPositions"];
            }
            set
            {
                ViewState["AliveCellPositions"] = value;
            }
        }
        protected SavedGame SavedGame
        {
            get
            {
                return (SavedGame)ViewState["SavedGame"];
            }
            set
            {
                ViewState["SavedGame"] = value;
            }
        }


        #endregion persistance

        protected void Page_Load ( object sender, EventArgs e )
        {
            if (Page.IsPostBack)
            {
                _putButtonsOnGrid();
            }
            else
            {
                _makeNewGameAndLayoutGrid();
            }

        }

        #region pagemethods
        protected void UpdateGameGridRowsColumns ( object sender, EventArgs e )
        {
            if (_trySetRowsColumnsForNewGame())
            {
                ButtonMakeGameGrid.Enabled = true;
            }
            else
            {
                ButtonMakeGameGrid.Enabled = false;
            }

        }
        protected void SaveGame ( object sender, EventArgs e )
        {
            _saveGame();
        }
        protected void ResetGameAndLayoutGrid ( object sender, EventArgs e )
        {
            _makeNewGameAndLayoutGrid();
            Panel panel = (Panel)FindControl("PanelSaveGameArea");
            panel.Visible = false;

        }
        private void _makeNewGameAndLayoutGrid ()
        {
            AliveCellPositions = null;
            if (_trySetRowsColumnsForNewGame())
            {
                Game game = new Game();
                game.StartNewGame(int.Parse(TextBoxRows.Text), int.Parse(TextBoxColumns.Text));
                Game = game;
                _layoutEmptyGameGrid();
                CheckBoxTimeOnOff.Checked = false;

                SavedGame = new SavedGame();
                SavedGame.Rows =Game.GameBoard.Rows;
                SavedGame.Columns = Game.GameBoard.Columns;
               
            }
        }
     
        private void _layoutEmptyGameGrid ()
        {
            if (_trySetRowsColumnsForNewGame())
            {
                GridAreaPanel.Controls.Clear();
                _putButtonsOnGrid();
                _putMessage(string.Format("Click to make cells alive or dead."));
            }
        }
        private void _setAliveOnLayoutGameGrid ( List<GridPosition> alivePositions )
        {
            if (alivePositions != null)
            {
                if (alivePositions.Count > 0)
                {
                    foreach (var aliveCellPosition in alivePositions)
                    {
                        var control = this.FindControl(string.Format("{0}_{1}", aliveCellPosition.Row, aliveCellPosition.Column));
                        _setButtonControlAliveDeadStyling((Button)control);
                    }
                }
            }
        }
        protected void GetNextGeneration ( object sender, EventArgs e )
        {
            _getNextGeneration();
        }

        #endregion pagemethods
        #region timer methods

        protected void UpdateTimerInterval ( object sender, EventArgs e )
        {
            int seconds;
            if (int.TryParse(TextBoxTimer.Text, out seconds))
            {
                Timer timer = (Timer)FindControl("Timer1");
                timer.Interval = 100 * seconds;
            }
        }
        protected void TurnTimerOnOff ( object sender, EventArgs e )
        {
            _turnTimerOnOff();

        }
        private void _turnTimerOnOff ()
        {
            if (CheckBoxTimeOnOff.Checked)
            {
                Timer timer = (Timer)FindControl("Timer1");
                timer.Enabled = true;
            }
            else
            {
                Timer timer = (Timer)FindControl("Timer1");
                timer.Enabled = false;
            }
        }

        protected void _turnTimerOff ()
        {
            Timer timer = (Timer)FindControl("Timer1");
            timer.Enabled = false;
        }
        #endregion

        protected void _getNextGeneration ()
        {
            Game.GetNextGeneration();
            _layoutEmptyGameGrid();
            _setAliveOnLayoutGameGrid(Game.AlivePositions);
            if (Game.SteadyStateGeneration != null)
            {
                _turnTimerOff();
                _putMessage(string.Format(" The current generation is {0} and {1} at generation {2}", Game.CurrentGeneration, Game.GameState.ToString(), Game.SteadyStateGeneration));

                SavedGame.EndGeneration = (int)Game.SteadyStateGeneration;
                SavedGame.EndState = Game.GameState;

                Panel panel = (Panel)FindControl("PanelSaveGameArea");
                panel.Visible = true;
            }
            else
            {
                _putMessage(string.Format(" The current generation is {0}", Game.CurrentGeneration));
            }
            Game = Game;
        }
        private void _saveGame ()
        {
            Game.SavedGames.Add(SavedGame);
            SavedGame = null;
            Panel panel = (Panel)FindControl("PanelSaveGameArea");
            panel.Visible = false;
         
            _putMessage(string.Format("There are now {0} saved games.", Game.SavedGames.Count));

        }
        private void _putMessage ( string message )
        {
            var messageArea = FindControl("gameMessageArea");
            messageArea.Controls.Clear();
            messageArea.Controls.Add(new LiteralControl(message));

        }
        protected bool _trySetRowsColumnsForNewGame ()
        {
            int rows;
            bool rowsOk = int.TryParse(TextBoxRows.Text, out rows);

            int columns;
            bool columnsOk = int.TryParse(TextBoxColumns.Text, out columns);

            if (rowsOk && rows > 0 && columnsOk && columns > 0)
            {
                _putMessage(string.Format("Valid Row Column ranges"));
                return true;
            }
            else
            {
                _putMessage(string.Format("Rows and/or Columns must be greater than 0"));
                return false;
            }
        }

        void _putButtonsOnGrid ( bool enableClick = true )
        {
            _grid = Game.GameBoard;
            for (int r = 0; r < _grid.Rows; r++)
            {
                GridAreaPanel.Controls.Add(new LiteralControl("<div>"));
                for (int c = 0; c < _grid.Columns; c++)
                {
                    var btn = new Button();
                    btn.ID = string.Format("{0}_{1}", r, c);
                    btn.Click += _btn_Click;
                    btn.Load += _btn_Load;
                    btn.Enabled = enableClick;

                    GridAreaPanel.Controls.Add(btn);
                }
                GridAreaPanel.Controls.Add(new LiteralControl("</div>"));
            }

        }
        void _btn_Load ( object sender, EventArgs e )
        {
            var control = (Button)sender;
            _setButtonControlAliveDeadStyling(control);
        }
        private void _setButtonControlAliveDeadStyling ( Button control )
        {
            int r;
            int c;
            _getRowColumnFromControlId(control, out r, out c);

            control.Height = 20;
            control.Width = 20;

            if (_grid.getCell(r, c).IsAlive)
            {
                control.Text = "O";
                control.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                control.Text = "X";
                control.BackColor = (new Button()).BackColor;
            }
        }
        void _btn_Click ( object sender, EventArgs e )
        {
            var control = (Control)sender;
            int r;
            int c;
            _getRowColumnFromControlId(control, out r, out c);
            if (_grid.getCell(r, c).IsAlive)
            {
                _grid.setDead(r, c);
            }
            else
            {
                _grid.setAliveCell(r, c);
            }
            _setButtonControlAliveDeadStyling((Button)control);
            AliveCellPositions = _grid.getAliveCellPositions();
            SavedGame.InitalAlivePositions = AliveCellPositions;

        }

        private void _getRowColumnFromControlId ( Control control, out int row, out int column )
        {
            var rc = control.ID.Split('_');
            row = int.Parse(rc[0]);
            column = int.Parse(rc[1]);
        }


    }
}