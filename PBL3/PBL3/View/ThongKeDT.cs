using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.BLL;
namespace PBL3
{
    public partial class ThongKeDT : Form
    {
        public ThongKeDT()
        {
            InitializeComponent();
            int year = DateTime.Now.Year;
            SetCBB(cbbStart);
            SetCBB(cbbEnd);
            SetCBB(cbbYear);
            ShowChart1(year);
            ShowChart(DateTime.Now.Year);
        }
        public void ShowChart(int year)
        {
            if (year == 0) year = ((CBBItem)cbbYear.SelectedItem).Value;
            else cbbYear.Text = year.ToString();
            label4.Text = "Biểu đồ thể hiện chi tiết doanh thu năm " + year.ToString();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart1.Series.Add("Doanh thu");
            chart1.Series["Doanh thu"].Points.AddXY("T1", BLL_Bill.INSTANCE.GetRevenue(year,1).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T2", BLL_Bill.INSTANCE.GetRevenue(year, 2).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T3", BLL_Bill.INSTANCE.GetRevenue(year, 3).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T4", BLL_Bill.INSTANCE.GetRevenue(year, 4).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T5", BLL_Bill.INSTANCE.GetRevenue(year, 5).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T6", BLL_Bill.INSTANCE.GetRevenue(year, 6).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T7", BLL_Bill.INSTANCE.GetRevenue(year, 7).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T8", BLL_Bill.INSTANCE.GetRevenue(year, 8).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T9", BLL_Bill.INSTANCE.GetRevenue(year, 9).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T10", BLL_Bill.INSTANCE.GetRevenue(year, 10).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T11", BLL_Bill.INSTANCE.GetRevenue(year, 11).ToString());
            chart1.Series["Doanh thu"].Points.AddXY("T12", BLL_Bill.INSTANCE.GetRevenue(year, 12).ToString());
        }
        public void SetCBB(ComboBox cbb)
        {
            for(int i = BLL_Bill.INSTANCE.GetYear(); i <= DateTime.Now.Year; i++)
            {
                cbb.Items.Add(new CBBItem
                {
                    Text = i.ToString(),
                    Value = i
                });
            }
            cbb.SelectedIndex = 0;
        }
        public void ShowChart1(int year)
        {
            int start = 0, end = 0;
            if(year != 0)
            {
                start = year; end = year;
                cbbStart.Text = DateTime.Now.Year.ToString();
                cbbEnd.Text = DateTime.Now.Year.ToString();
            }
            else
            {
                 start = ((CBBItem)cbbStart.SelectedItem).Value;
                 end = ((CBBItem)cbbEnd.SelectedItem).Value;
            }
            
            var objChart = chart2.ChartAreas[0];

            objChart.AxisX.Minimum = 1;
            objChart.AxisX.Maximum = 12;

            for (int i = start; i <=end; i++)
                chart2.Series.Add(i.ToString());

            for (int j = start; j <= end; j++)
                for (int i = 1; i <= 12; i++)
                {
                    chart2.Series[j.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart2.Series[j.ToString()].Points.AddXY(i, BLL_Bill.INSTANCE.GetRevenue(j, i).ToString());
                }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            ShowChart1(0);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            ShowChart(0);
        }
    }
}
