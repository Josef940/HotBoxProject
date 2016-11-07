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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotBox.BLL;
using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Logic;
namespace HotBoxSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Facade facade = Facade.Instance;
        static TrendProject hotbox = null;
        List<HotBoxValues> hotboxValues = null;
        public MainWindow()
        {
            InitializeComponent();
            hotbox = facade.GetDataBridge().GetHotBoxData();
            var modules = facade.GetDataLogic().GetModules(hotbox);
            hotboxValues = facade.GetDataLogic().GetHotBoxValues(hotbox);
            moduleDataGrid.ItemsSource = hotboxValues;

        }

        public void UpdateDataGrid()
        {

        }

    }

}
