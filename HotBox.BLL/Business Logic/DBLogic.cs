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
        readonly int MAX_CHART_POINT = 100;
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
            var pointdivisor = pointvalues.Max(x => x.DataValue) / MAX_CHART_POINT;
            foreach (var item in pointvalues)
            {
                double minutedifference = (item.DataTime - startDate).TotalMinutes;
                pointcollection.Add(GetChartPoint(minutedifference, item.DataValue, pointdivisor));
            }
            var pointAndDiv = new PointCollectionAndDivisor
            {
                PointCollection = pointcollection,
                Divisor = pointdivisor
            };
            return pointAndDiv;
        }

        public Point GetChartPoint(double x, double y, double divisor)
        {
            // The added value can be changed if it is wished to change the height
            // of the ValueChartWindow.xaml
            y = -y + MAX_CHART_POINT;
            return new Point(x, y);
        }
    }
}