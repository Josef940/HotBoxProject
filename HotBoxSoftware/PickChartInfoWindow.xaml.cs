using HotBox.BLL.Business_Entities;
using HotBox.BLL.Business_Entities.ViewModels;
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
    /// Interaction logic for PickChartInfoWindow.xaml
    /// </summary>
    public partial class PickChartInfoWindow : Window
    {
        public PickChartInfoWindow(HotBoxValues hbvalues)
        {
            InitializeComponent();
        }
    }
}
