using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Entities.DBViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HotBox.BLL.Business_Logic
{
    public class DBLogic
    {
        Facade facade = Facade.Instance;
        readonly int xPositiveLength = 350;
        readonly int yPositiveLength = 100;
        public PointCollectionAndDivisor GetPointValuesForChart(string pointname, int minutes)
        {
            List<PointValue> values = new List<PointValue>();
            var dbpoints = facade.GetDBBridge().GetLatestPointValues(pointname, minutes);

            foreach (var item in dbpoints)
                values.Add(facade.GetBEConverter().PointValueConverter(item));

            var pointCollection = PointValuesToPointCollection(values);
            return pointCollection;
        }

        // Returns a viable PointCollection to be used for a chart
        public PointCollectionAndDivisor PointValuesToPointCollection(List<PointValue> pointvalues)
        {
            if (pointvalues == null || pointvalues.Count == 0)
                return null;

            var pointcollection = new PointCollection();
            DateTime startDate = pointvalues[0].DataTime;

            var yPointDivisor = Convert.ToInt32(Math.Ceiling(pointvalues.Max(x => x.DataValue) / yPositiveLength));
            var xPointDivisor = CalculateXDivisor(pointvalues);
            foreach (var item in pointvalues)
            {
                double minutedifference = (item.DataTime - startDate).TotalMinutes;
                pointcollection.Add(GetChartPoint(minutedifference, item.DataValue, xPointDivisor, yPointDivisor));
            }

            var pointAndDiv = new PointCollectionAndDivisor
            {
                PointCollection = pointcollection,
                yDivisor = yPointDivisor,
                xDivisor = xPointDivisor
            };
            return pointAndDiv;
        }

        public int CalculateXDivisor(List<PointValue> pointvalues)
        {
            var minutedifference = new List<double>();
            DateTime startDate = pointvalues[0].DataTime;
            foreach (var item in pointvalues)
            {
                minutedifference.Add((item.DataTime - startDate).TotalMinutes);
            }

            int xDivisor = Convert.ToInt32(Math.Ceiling(minutedifference.Max() / xPositiveLength));

            return xDivisor;
        }

        public Point GetChartPoint(double x, double y, int xdivisor, int ydivisor)
        {
            // The added value can be changed if it is wished to change the height
            // of the ValueChartWindow.xaml
            y /= ydivisor;
            y = (-y + yPositiveLength);
            x /= xdivisor;
            return new Point(x, y);
        }
    }
}