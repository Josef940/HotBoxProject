using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Entities.DBViewModels;
using HotBox.BLL.Business_Entities.ViewModels;
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
        double yPositive;
        string pointName;
        int minutes;
        int yDivisor = 1;
        int xDivisor = 1;
        double xLength, yLength, yLengthInMinus;
        readonly int XUNIT = 10;
        readonly int YUNIT = 10;
        readonly int AXIS_POINT_LINE_LENGTH = 4;
        double pointLineMarginLeft;
        double pointLineMarginTop;
        public ValueChartWindow(string pointname, int minutes)
        {
            InitializeComponent();
            this.pointName = pointname;
            this.minutes = minutes;

            xLength = xLine.Points[1].X;
            yLength = yLine.Points[1].Y;

            // Has to be set manually by checking how much of the x-line is on the negative side
            yLengthInMinus = 20;

            yPositive = yLength - yLengthInMinus;
            pointLineMarginLeft = xLine.Margin.Left;
            pointLineMarginTop = xLine.Margin.Top;
            //
            var pointvalues = facade.GetDBLogic().GetPointValuesForChart(pointName, minutes);
            if (pointvalues != null)
            {
                ValueChart.Points = pointvalues.PointCollection;
                yDivisor = pointvalues.yDivisor;
                xDivisor = pointvalues.xDivisor;
                //Unit_TextBlock.Text = hbvalues.Unit;
                //SetChartPoints();
                GenerateChart();
            }
            else
                ValuesNotFound();
        }

        private void ValuesNotFound()
        {
            NoValues_Textblock.Visibility = Visibility.Visible;
        }

        private void GenerateChart()
        {
            //// Generates point-lines and numbers for the y-axis above 0
            //for (int i = 0; i < (yLength - yLengthInMinus) / YUNIT; i++)
            //{
            //    var tb = new TextBlock();
            //    tb.FontSize = 6;
            //    tb.Text = Convert.ToString((yPositive - i * (yPositive / YUNIT)) * yDivisor);
            //    tb.Margin = new Thickness(pointLineMarginLeft - 15, pointLineMarginTop + 15.5 + i * (yPositive / YUNIT) - yLengthInMinus, 0, 0);
            //    myCanvas.Children.Add(tb);

            //    var yPointLine = new Polyline();
            //    yPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
            //    yPointLine.Stroke = new SolidColorBrush(Colors.Black);
            //    yPointLine.Points = new PointCollection { new Point(-AXIS_POINT_LINE_LENGTH, i * YUNIT), new Point(AXIS_POINT_LINE_LENGTH, i * YUNIT) };
            //    myCanvas.Children.Add(yPointLine);
            //}
            //// Generates point-lines and numbers for the y-axis below 0
            //for (int i = 1; i < (yLengthInMinus / YUNIT) + 1; i++)
            //{
            //    var tb = new TextBlock();
            //    tb.FontSize = 6;
            //    tb.Text = Convert.ToString((-i * (yPositive / YUNIT)) * yDivisor);
            //    tb.Margin = new Thickness(pointLineMarginLeft - 15, pointLineMarginTop - 4.5 + i * YUNIT + yPositive, 0, 0);
            //    myCanvas.Children.Add(tb);

            //    var yPointLine = new Polyline();
            //    yPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
            //    yPointLine.Stroke = new SolidColorBrush(Colors.Black);
            //    yPointLine.Points = new PointCollection { new Point(-AXIS_POINT_LINE_LENGTH, i * YUNIT + yPositive), new Point(AXIS_POINT_LINE_LENGTH, i * YUNIT + yPositive) };
            //    myCanvas.Children.Add(yPointLine);
            //}
            //// Generates point-lines and numbers for the x-axis
            //for (int i = 1; i < (xLength / XUNIT) + 1; i++)
            //{
            //    if (i % 2 != 0)
            //    {
            //        var tb = new TextBlock();
            //        tb.FontSize = 6;
            //        tb.Text = Convert.ToString((i * XUNIT) * xDivisor);
            //        tb.Margin = new Thickness(i * XUNIT + pointLineMarginLeft - 5, pointLineMarginTop + yPositive + 5, 0, 0);
            //        myCanvas.Children.Add(tb);
            //    }

            //    var xPointLine = new Polyline();
            //    xPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
            //    xPointLine.Stroke = new SolidColorBrush(Colors.Black);
            //    xPointLine.Points = new PointCollection { new Point(i * XUNIT, -AXIS_POINT_LINE_LENGTH + yPositive), new Point(i * XUNIT, AXIS_POINT_LINE_LENGTH + yPositive) };
            //    myCanvas.Children.Add(xPointLine);
            //}

            GeneratePointsYAxisAboveZero();
            GeneratePointsYAxisBelowZero();
            GeneratePointsXAxis();
        }

        private void GeneratePointsXAxis()
        {
            for (int i = 1; i < (xLength / XUNIT) + 1; i++)
            {
                if (i % 2 != 0)
                {
                    var tb = new TextBlock();
                    tb.FontSize = 6;
                    tb.Text = Convert.ToString((i * XUNIT) * xDivisor);
                    tb.Margin = new Thickness(i * XUNIT + pointLineMarginLeft - 5, pointLineMarginTop + yPositive + 5, 0, 0);
                    myCanvas.Children.Add(tb);
                }

                var xPointLine = new Polyline();
                xPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
                xPointLine.Stroke = new SolidColorBrush(Colors.Black);
                xPointLine.Points = new PointCollection { new Point(i * XUNIT, -AXIS_POINT_LINE_LENGTH + yPositive), new Point(i * XUNIT, AXIS_POINT_LINE_LENGTH + yPositive) };
                myCanvas.Children.Add(xPointLine);
            }
        }

        private void GeneratePointsYAxisBelowZero()
        {
            for (int i = 1; i < (yLengthInMinus / YUNIT) + 1; i++)
            {
                var tb = new TextBlock();
                tb.FontSize = 6;
                tb.Text = Convert.ToString((-i * (yPositive / YUNIT)) * yDivisor);
                tb.Margin = new Thickness(pointLineMarginLeft - 15, pointLineMarginTop - 4.5 + i * YUNIT + yPositive, 0, 0);
                myCanvas.Children.Add(tb);

                var yPointLine = new Polyline();
                yPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
                yPointLine.Stroke = new SolidColorBrush(Colors.Black);
                yPointLine.Points = new PointCollection { new Point(-AXIS_POINT_LINE_LENGTH, i * YUNIT + yPositive), new Point(AXIS_POINT_LINE_LENGTH, i * YUNIT + yPositive) };
                myCanvas.Children.Add(yPointLine);
            }
        }
        private void GeneratePointsYAxisAboveZero()
        {
            for (int i = 0; i < (yLength - yLengthInMinus) / YUNIT; i++)
            {
                var tb = new TextBlock();
                tb.FontSize = 6;
                tb.Text = Convert.ToString((yPositive - i * (yPositive / YUNIT)) * yDivisor);
                tb.Margin = new Thickness(pointLineMarginLeft - 15, pointLineMarginTop + 15.5 + i * (yPositive / YUNIT) - yLengthInMinus, 0, 0);
                myCanvas.Children.Add(tb);

                var yPointLine = new Polyline();
                yPointLine.Margin = new Thickness(xLine.Margin.Left, xLine.Margin.Top, 0, 0);
                yPointLine.Stroke = new SolidColorBrush(Colors.Black);
                yPointLine.Points = new PointCollection { new Point(-AXIS_POINT_LINE_LENGTH, i * YUNIT), new Point(AXIS_POINT_LINE_LENGTH, i * YUNIT) };
                myCanvas.Children.Add(yPointLine);
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