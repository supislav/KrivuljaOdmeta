using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KrivuljaOdmeta
{
    public partial class MainForm : Form
    {
        private double _startTime;
        private double _increment;
        public MainForm()
        {
            InitializeComponent();
            DGVSettings();
            ChartSettings();
            ResetChart();
        }

        private void BtnNarisiKrivuljo_Click(object sender, EventArgs e)
        {
            ResetChart(); // Reset chart
            dgv.Rows.Clear(); // Reset data grid view

            _startTime = Convert.ToDouble(startTime.Text); // Začetni čas
            _increment = Convert.ToDouble(timeIncrement.Text);// Inkrement
            double time = _startTime; //Čas

            Series series1 = new Series(); // Nova črta
            series1.ChartType = SeriesChartType.Spline; //Linijski graf
            series1.BorderWidth = 5;

            // Računanje in risanje krivulje
            for (int i = 0; i < Convert.ToInt32(steviloTock.Text); i++)
            {
                // Izračun točke
                Point point = Point.Calculate(time);

                // Risanje
                series1.Points.AddXY(point.X1, point.Y1);

                // Prikaz v DGV
                dgv.Rows.Add(time, point.X1, point.Y1);
                time = time += _increment; // Inkrementacija časa
            }

            // Dodajanje serije točk na graf
            chart.Series.Add(series1);

        }

        #region SETTINGS AND TOOLS
        private void DGVSettings()
        {
            // DGV definiranje stolpcev
            dgv.ColumnCount = 3;
            dgv.Columns[0].Name = "t[s]";
            dgv.Columns[1].Name = "x1[m]";
            dgv.Columns[2].Name = "y1[m]";
            // DGV value types
            dgv.Columns[0].ValueType = typeof(int);
            dgv.Columns[1].ValueType = typeof(double);
            dgv.Columns[2].ValueType = typeof(double);
            // DGV sirina stolpcev
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // DGV brez spreminjanja stolcev
            dgv.AllowUserToOrderColumns = false;
            // Dve decimalki
            for (int i = 0; i <= 2; i++) dgv.Columns[i].DefaultCellStyle.Format = "0.##";
            // DGV Row Headers
            dgv.RowHeadersVisible = true;
            dgv.RowPostPaint += Dgv_RowPostPaint;
        }

        private void ChartSettings()
        {
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "#";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "#";

        }

        private void ResetChart()
        {
            //Reset grafa
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();
            ChartArea defaultChartArea = new ChartArea();
            chart.ChartAreas.Add(defaultChartArea);
        }

        private void Dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        #endregion

    }
}
