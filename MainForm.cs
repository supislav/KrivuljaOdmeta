using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrivuljaOdmeta
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // DGV definiranje stolpcev
            dgv.ColumnCount = 3;
            dgv.Columns[0].Name = "t[s]";
            dgv.Columns[1].Name = "x1[m]";
            dgv.Columns[2].Name = "y1[m]";
            // DGV sirina stolpcev
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // DGV brez spreminjanja stolcev
            dgv.AllowUserToOrderColumns = false;
        }

        private void BtnNarisiKrivuljo_Click(object sender, EventArgs e)
        {
            // Risanje krivulje
            for (int i = 0; i < length; i++)
            {

            }

        }

        

    }
}
