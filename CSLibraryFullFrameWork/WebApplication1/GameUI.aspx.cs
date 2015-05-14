using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryFull;

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
            AliveCellPositions = null;
            _resetLayoutGameGrid();
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
        var _newGrid=    _grid.getNextGeneration();
        AliveCellPositions = _newGrid.getAliveCellPositions();
            _resetLayoutGameGrid();
            _grid.setAliveCells(AliveCellPositions);

            var messageArea=FindControl("gameMessageArea");
           //<<
            if (AliveCellPositions != null)
            {
                foreach (var AliveCellPosition in AliveCellPositions)
                {
                    var control = this.FindControl(string.Format("{0}_{1}", AliveCellPosition.Row, AliveCellPosition.Column));
                    _setButtonControlText((Button)control);
                }
            }
        }
        #endregion pagemethods
        protected bool _trySetGridRowsColumns ()
        {
            int rows;
            bool rowsOk = int.TryParse(TextBoxRows.Text, out rows);

            int columns;
            bool columnsOk = int.TryParse(TextBoxCoulumns.Text, out columns);

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
       
        void _putButtonsOnGrid (bool enableClick=true)
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
            _setButtonControlText(control);
        }
        private void _setButtonControlText ( Button control )
        {
            int r;
            int c;
            _getRowColumnFromControlId(control, out r, out c);
            if (_grid.getCell(r, c).IsAlive)
            {
                control.Text = "O";
                control.BackColor =System.Drawing.Color.Green;
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
            _setButtonControlText((Button)control);
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