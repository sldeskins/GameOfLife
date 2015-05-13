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
        protected void Page_Load ( object sender, EventArgs e )
        {
            if (Page.IsPostBack)
            {
                if (NumberOfRows != null && NumberOfColumns != null)
                {
                    _grid = new Grid(NumberOfRows.Value, NumberOfColumns.Value);
                    _grid.setAliveCells(AliveCellPositions);
                    _layoutGrid();
                }
            }

        }

        protected void UpdateGameGridRowsColumns ( object sender, EventArgs e )
        {
            if (trySetGridRowsColumns())
            {
                ButtonMakeGameGrid.Enabled = true;
            }
            else
            {
                ButtonMakeGameGrid.Enabled = false;
            }

        }
        protected bool trySetGridRowsColumns ()
        {
            int rows;
            bool rowsOk = int.TryParse(TextBoxRows.Text, out rows);
            NumberOfRows = rows;

            int columns;
            bool columnsOk = int.TryParse(TextBoxCoulumns.Text, out columns);
            NumberOfColumns = columns;

            if (rowsOk && columnsOk)
            {
                _grid = new Grid(rows, columns);
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void LayoutGameGrid ( object sender, EventArgs e )
        {
            if (trySetGridRowsColumns())
            {

                _layoutGrid();

            }
        }
        void _layoutGrid ()
        {
            for (int r = 0; r < _grid.Rows; r++)
            {
                GridAreaPanel.Controls.Add(new LiteralControl("<div>"));
                for (int c = 0; c < _grid.Columns; c++)
                {
                    var btn = new Button();
                    btn.ID = string.Format("{0}_{1}", r, c);
                    btn.Click += btn_Click;
                    btn.Load += btn_Load;
                    GridAreaPanel.Controls.Add(btn);
                }
                GridAreaPanel.Controls.Add(new LiteralControl("</div>"));
            }
        }
        void btn_Load ( object sender, EventArgs e )
        {
            var control = (Button)sender;
            _setControlText(control);
        }
        private void _setControlText ( Button control )
        {

            int r;
            int c;
            getRowColumnFromControlId(control, out r, out c);
            if (_grid.getCell(r, c).IsAlive)
            {
                control.Text = "O";
            }
            else
            {
                control.Text = "X";
            }
        }
        void btn_Click ( object sender, EventArgs e )
        {
            var control = (Control)sender;
            int r;
            int c;
            getRowColumnFromControlId(control, out r, out c);
            if (_grid.getCell(r, c).IsAlive)
            {
                _grid.setDead(r, c);
            }
            else
            {
                _grid.setAliveCell(r, c);
            }
            AliveCellPositions = _grid.getAliveCellPositions();
            _setControlText((Button)control);
        }

        private void getRowColumnFromControlId ( Control control, out int row, out int column )
        {
            var rc = control.ID.Split('_');
            row = int.Parse(rc[0]);
            column = int.Parse(rc[1]);
        }
    }
}