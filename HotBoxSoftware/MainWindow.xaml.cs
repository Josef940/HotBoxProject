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
using HotBox.BE;
namespace HotBoxSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpGateway hbll = new HttpGateway();
        public MainWindow()
        {
            InitializeComponent();
            TrendProject data = hbll.GetHotBoxData();
            Button randomButton = new Button();
            randomButton.Height = 30;
            randomButton.Width = 60;
            randomButton.Content = "This is a button :D";
            mainGrid.Children.Add(randomButton);

            for(hbll.)
        }
    }
}
