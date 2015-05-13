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

        protected Panel NumberOfControls
        {
            get
            {
                return (Panel)ViewState["NumControls"];
            }
            set
            {
                ViewState["NumControls"] = value;
            }
        }
        protected void Page_Load ( object sender, EventArgs e )
        {
            if (Page.IsPostBack)
            {
                if (NumberOfControls != null)
                {
                    GridAreaPanel = NumberOfControls;
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

            int columns;
            bool columnsOk = int.TryParse(TextBoxCoulumns.Text, out columns);

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

                for (int r = 0; r < _grid.Rows; r++)
                {
                    GridAreaPanel.Controls.Add(new LiteralControl("<div>"));
                    for (int c = 0; c < _grid.Columns; c++)
                    {
                        var btn = new Button();
                        btn.ID = string.Format("{0}_{1}", r, c);
                        btn.Load += btn_Load;
                        GridAreaPanel.Controls.Add(btn);
                    }
                    GridAreaPanel.Controls.Add(new LiteralControl("</div>"));
                }

            }
        }

        void btn_Load ( object sender, EventArgs e )
        {
            var control = (Control)sender;
            var rc = control.ID.Split('_');
            int row = int.Parse(rc[0]);
            int columun = int.Parse(rc[1]);
        }


    }
}