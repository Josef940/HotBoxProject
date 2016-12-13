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
        string pointName;
        int minutes;
        public ValueChartWindow(string pointName, int minutes)
        {
            InitializeComponent();
            this.pointName = pointName;
            this.minutes = minutes;
            var pointvalues = facade.GetDBLogic().GetPointValuesForChart(pointName, minutes);
            ValueChart.Points = pointvalues;
            //SetChartPoints();
        }

        private Task SetChartPoints()
        {
            return Task.Factory.StartNew(() => facade.GetDBLogic().GetPointValuesForChart(pointName, minutes))
                .ContinueWith(r => ValueChart.Points = r.Result,
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
