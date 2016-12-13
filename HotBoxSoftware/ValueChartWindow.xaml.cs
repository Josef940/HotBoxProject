using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.BLL.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotBoxSoftware
{
    /// <summary>
    /// Interaction logic for ValueChartWindow.xaml
    /// </summary>
    public partial class ValueChartWindow : Window
    {
        Facade facade = Facade.Instance;
        readonly int MAX_CHART_POINT = 100;
        string pointName;
        int minutes;
        double divisor = 1;
        List<Tuple<int, int>> xSideValues = new List<Tuple<int, int>>();
        public ValueChartWindow(string pointName, int minutes)
        {
            InitializeComponent();
            this.pointName = pointName;
            this.minutes = minutes;
            var pointvalues = facade.GetDBLogic().GetPointValuesForChart(pointName, minutes);
            ValueChart.Points = pointvalues.PointCollection;
            divisor = pointvalues.Divisor;
            //SetChartPoints();
            SetChartLineValues();
        }

        public void SetChartLineValues()
        {
            for (int i = 0; i < 10; i++)
            {
                var tb = new TextBlock();
                tb.FontSize = 6;
                tb.Text = Convert.ToString(MAX_CHART_POINT - i * (MAX_CHART_POINT / 10));
                tb.Margin = new Thickness(10, 35.5 + i * (MAX_CHART_POINT / 10), 0, 0);
                myCanvas.Children.Add(tb);

                var yPointLine = new Polyline();
                yPointLine.Margin = new Thickness(25, 40, 0, 0);
                yPointLine.Stroke = new SolidColorBrush(Colors.Black);
                yPointLine.Points = new PointCollection { new Point(-5, i * 10), new Point(5, i * 10) };
                myCanvas.Children.Add(yPointLine);
            }

            for (int i = 0; i < (xLine.Points[1].X / 10)+1; i++)
            {
                var xPointLine = new Polyline();
                xPointLine.Margin = new Thickness(25, 40, 0, 0);
                xPointLine.Stroke = new SolidColorBrush(Colors.Black);
                xPointLine.Points = new PointCollection { new Point(i * 10, -5 + MAX_CHART_POINT), new Point(i * 10, 5 + MAX_CHART_POINT) };
                myCanvas.Children.Add(xPointLine);
            }
        }

        private Task SetChartPoints()
        {
            return Task.Factory.StartNew(() => facade.GetDBLogic().GetPointValuesForChart(pointName, minutes))
                .ContinueWith(r => ValueChart.Points = r.Result.PointCollection,
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}