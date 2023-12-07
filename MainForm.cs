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

            Series series1 = new Series(); // Serija za x1-y1
            Series series2 = new Series(); // Serija za x2-y2
            series1.ChartType = SeriesChartType.Spline; // Chart type
            series2.ChartType = SeriesChartType.Spline; // Chart type
            series1.BorderWidth = 5;
            series2.BorderWidth = 5;

            // Računanje in risanje krivulje
            for (int i = 0; i < Convert.ToInt32(steviloTock.Text); i++)
            {
                // Izračun točke
                Point point = Point.Calculate(time);
                // Risanje
                series1.Points.AddXY(point.X1, point.Y1);
                series2.Points.AddXY(point.X2, point.Y1); //Y1 = Y2
                // Prikaz v DGV
                dgv.Rows.Add(time, point.X1, point.Y1, point.X2, point.Y1); //Y1 = Y2
                time = time += _increment; // Inkrementacija časa
            }
            // Dodajanje serije točk na graf
            chart.Series.Add(series1);
            chart.Series.Add(series2);
        }

        #region SETTINGS AND TOOLS
        private void DGVSettings()
        {
            // DGV definiranje stolpcev
            dgv.ColumnCount = 5;
            dgv.Columns[0].Name = "t[s]";
            dgv.Columns[1].Name = "x1[m]";
            dgv.Columns[2].Name = "y1[m]";
            dgv.Columns[3].Name = "x2[m]";
            dgv.Columns[4].Name = "y2[m]";
            // DGV value types
            dgv.Columns[0].ValueType = typeof(int);
            dgv.Columns[1].ValueType = typeof(double);
            dgv.Columns[2].ValueType = typeof(double);
            dgv.Columns[3].ValueType = typeof(double);
            dgv.Columns[4].ValueType = typeof(double);
            // DGV sirina stolpcev
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // DGV brez spreminjanja stolcev
            dgv.AllowUserToOrderColumns = false;
            // Dve decimalki
            for (int i = 0; i <= 4; i++) dgv.Columns[i].DefaultCellStyle.Format = "0.#####";
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
