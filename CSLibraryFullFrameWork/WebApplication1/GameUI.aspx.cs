using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryFull;
using System.Threading;

namespace WebApplication1
{
    public partial class GameUI : System.Web.UI.Page
    {
        #region persistance
        private ClassLibraryFull.Grid _grid;

        protected int? NumberOfRows
        {
            get
            {
                return (int?)ViewState["NumberOfRows"];
            }
            set
            {
                ViewState["NumberOfRows"] = value;
            }
        }
        protected int? NumberOfColumns
        {
            get
            {
                return (int?)ViewState["NumberOfColumns"];
            }
            set
            {
                ViewState["NumberOfColumns"] = value;
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
        private int GenerationNumber
        {
            get
            {
                return (int)ViewState["GenerationNumber"];
            }
            set
            {
                ViewState["GenerationNumber"] = value;
            }
        }
        //private bool Run
        //{
        //    get
        //    {
        //        return (bool)ViewState["Run"];
        //    }
        //    set
        //    {
        //        ViewState["Run"] = value;
        //    }
        //}
        #endregion persistance

        protected void Page_Load ( object sender, EventArgs e )
        {
            if (Page.IsPostBack)
            {
                if (NumberOfRows != null && NumberOfColumns != null)
                {
                    _grid = new Grid(NumberOfRows.Value, NumberOfColumns.Value);
                    _grid.setAliveCells(AliveCellPositions);
                    _putButtonsOnGrid();
                }
                //if (Run)
                //{
                //    System.Web.UI.Timer timer = new System.Web.UI.Timer();

                //    timer.Enabled = true;

                //    StartNextGeneration(null, new EventArgs());
                //}
            }
            else
            {
              //  Run = false;
            }

        }

        #region pagemethods
        protected void UpdateGameGridRowsColumns ( object sender, EventArgs e )
        {
            if (_trySetGridRowsColumns())
            {
                ButtonMakeGameGrid.Enabled = true;
            }
            else
            {
                ButtonMakeGameGrid.Enabled = false;
            }

        }
        protected void ResetLayoutGameGrid ( object sender, EventArgs e )
        {
            GenerationNumber = 0;
            AliveCellPositions = null;
            _resetLayoutGameGrid();
            _putMessage(string.Format("Click to make cells alive or dead."));

        }
        private void _resetLayoutGameGrid ()
        {
            if (_trySetGridRowsColumns())
            {
                GridAreaPanel.Controls.Clear();
                _putButtonsOnGrid();
            }
        }
        protected void GetNextGeneration ( object sender, EventArgs e )
        {
            _getNextGeneration();
        }
        //protected void StartNextGeneration ( object sender, EventArgs e )
        //{
        //    Run = true;

        //    _getNextGeneration();
        //    Thread.Sleep(1000);
         
        //}
        //protected void StopNextGeneration ( object sender, EventArgs e )
        //{
        //    Run = false;
        //}
        #endregion pagemethods

 
        protected void _getNextGeneration ()
        {
            GenerationNumber++;
            var _newGrid = _grid.getNextGeneration();
            AliveCellPositions = _newGrid.getAliveCellPositions();
            _resetLayoutGameGrid();
            _grid.setAliveCells(AliveCellPositions);

            _putMessage(string.Format(" The current generation is {0}", GenerationNumber));

            if (AliveCellPositions != null)
            {
                foreach (var AliveCellPosition in AliveCellPositions)
                {
                    var control = this.FindControl(string.Format("{0}_{1}", AliveCellPosition.Row, AliveCellPosition.Column));
                    _setButtonControlAliveDeadStyling((Button)control);
                }
            }}
        private void _putMessage ( string message )
        {
            var messageArea = FindControl("gameMessageArea");
            messageArea.Controls.Clear();
            messageArea.Controls.Add(new LiteralControl(message));

        }
        protected bool _trySetGridRowsColumns ()
        {
            int rows;
            bool rowsOk = int.TryParse(TextBoxRows.Text, out rows);

            int columns;
            bool columnsOk = int.TryParse(TextBoxColumns.Text, out columns);

            if (rowsOk && columnsOk)
            {
                _grid = new Grid(rows, columns);
                NumberOfRows = rows;
                NumberOfColumns = columns;
                return true;
            }
            else
            {
                return false;
            }
        }

        void _putButtonsOnGrid ( bool enableClick = true )
        {
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
        }

        private void _getRowColumnFromControlId ( Control control, out int row, out int column )
        {
            var rc = control.ID.Split('_');
            row = int.Parse(rc[0]);
            column = int.Parse(rc[1]);
        }


    }
}