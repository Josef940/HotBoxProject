using HotBox.BLL.Business_Entities;
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
    /// Interaction logic for ValueWindow.xaml
    /// </summary>
    public partial class ValueWindow : Window
    {
        Facade facade = Facade.Instance;
        HotBoxValues hotboxvalues;
        public ValueWindow(HotBoxValues hotboxvalues)
        {
            InitializeComponent();
            if (hotboxvalues == null)
                Close();
            this.hotboxvalues = hotboxvalues;
            TextBlockModuleName.Text = hotboxvalues.Label;
            TextBoxValue.Text = Convert.ToString(hotboxvalues.Value);
            TextBlockUnit.Text = hotboxvalues.Unit;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (facade.GetDataLogic().ValueIsADouble(TextBoxValue.Text))
            {
                if (facade.GetDataXMLBridge().PostHotBoxValue(hotboxvalues.Module, TextBoxValue.Text))
                {
                    MessageBox.Show("Value has been successfully set");
                    Close();
                }else
                    MessageBox.Show("An error has occurred! Could not set value");
            }
            else
                MessageBox.Show("Not a valid  number");
        }
    }
}
