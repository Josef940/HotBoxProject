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
using System.Threading;
using System.Windows.Threading;
using HotBox.BLL.Business_Entities.ViewModels;

namespace HotBoxSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Facade facade = Facade.Instance;
        List<HotBoxValues> hotboxValues = null;
        List<HotBoxValues> writeablehotboxValues = null;
        readonly int UPDATE_SECONDS = 15;
        Dictionary<string, string> SPdictionary = null;
        // Amount of seconds the error message will show on the screen
        readonly int ERROR_MESSAGE_SECONDS = 12;
        public MainWindow()
        {
            InitializeComponent();
            moduleDataGrid.ItemsSource = hotboxValues;
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            UpdateDataGridLoop();
            Time_TextBlock.Text = "(" + facade.GetViewLogic().MinutesToTimeText(Convert.ToInt32(0)) + ")";
        }

        private Task UpdateDataGridLoop()
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    InvokeDispatcher(() => TextBlockLoading.Visibility = Visibility.Visible);
                    bool updated = facade.GetXMLLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues);
                    InvokeDispatcher(() => TextBlockLoading.Visibility = Visibility.Hidden);
                    if (SPdictionary == null)
                        PopulateSPdictionary();
                    if (updated)
                        InvokeDispatcher(() => RefreshHotBoxDataGrid());
                    else
                        Task.Factory.StartNew(() => TextBlockErrorToVisible());
                    Thread.Sleep(TimeSpan.FromSeconds(UPDATE_SECONDS));
                }
            });
        }

        private Task PopulateSPdictionary()
        {
            return Task.Factory.StartNew(() =>
            {
                SPdictionary = facade.GetDataXMLBridge().StoPDictionary();
            }).ContinueWith(_ =>
            {
                if (SPdictionary == null)
                    InvokeDispatcher(() => DictionaryFailed());
                else
                {
                    InvokeDispatcher(() => DictionarySucceeded());
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    InvokeDispatcher(() => Chart_TextBlock.Visibility = Visibility.Hidden);
                }
            });
        }

        private void DictionarySucceeded()
        {
            Chart_TextBlock.Foreground = new SolidColorBrush(Colors.Green);
            Chart_TextBlock.Text = "Charts can now be accessed";
        }

        private void DictionaryFailed()
        {
            Chart_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
            Chart_TextBlock.Text = "Failed loading chart information!";
        }

        // Starts new thread and
        // sets the error message to visible for ERRORMESSAGESECONDS seconds
        private Task TextBlockErrorToVisible()
        {
            return Task.Factory.StartNew(() =>
            {
                InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Visible);
                Thread.Sleep(TimeSpan.FromSeconds(ERROR_MESSAGE_SECONDS));
                InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Hidden);
            });
        }

        private void RefreshHotBoxDataGrid()
        {
            moduleDataGrid.ItemsSource = hotboxValues;
            moduleDataGrid.Items.Refresh();
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            DataGridwriteableModule.Items.Refresh();
            DataGridwriteableModule.Columns.RemoveAt(4);
        }
        // Invokes the dispatcher object to access GUI elements from another thread than the main thread.
        // Code should be passed to the parameter by using a lambda expression
        private void InvokeDispatcher(Action codetoexecute)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    codetoexecute();
                });
        }

        private void OpenValueWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vwin = new ValueWindow(DataGridwriteableModule.SelectedItem as HotBoxValues);
            vwin.Show();
        }

        private void Odpen_PickChartInfoWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var chartinfowin = new PickChartInfoWindow(DataGridwriteableModule.SelectedItem as HotBoxValues);
            chartinfowin.Show();

        }
        /*
        private void Open_ValueChartWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SPdictionary == null)
            {
                int minutes = Convert.ToInt32(Minutes_TextBox.Text);
                var spoint = (DataGridwriteableModule.SelectedItem as HotBoxValues).Module;
                string ppoint;
                if (SPdictionary.ContainsKey(spoint))
                    ppoint = SPdictionary[spoint];
                else
                    ppoint = "";
                var chartwin = new ValueChartWindow(ppoint, minutes);
                chartwin.Show();
            }
        }*/
        private void Second_Open_ValueChartWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SPdictionary != null)
            {
                int minutes = Convert.ToInt32(Minutes_TextBox.Text);
                var spoint = (moduleDataGrid.SelectedItem as HotBoxValues).Module;
                string ppoint;
                if (SPdictionary.ContainsKey(spoint))
                    ppoint = SPdictionary[spoint];
                else
                    ppoint = "";
                var chartwin = new ValueChartWindow(ppoint, minutes);
                chartwin.Show();
            }
        }

        private void SetValue_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridwriteableModule.SelectedItem != null)
            {
                var hotboxValue = DataGridwriteableModule.SelectedItem as HotBoxValues;
                new ValueWindow(hotboxValue).Show();
            }
        }

        private void Minutes_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var validInteger = facade.GetViewLogic().GetValidIntegerAsString(Minutes_TextBox.Text, 5);
            Minutes_TextBox.Text = validInteger;
            Minutes_TextBox.SelectionStart = Minutes_TextBox.Text.Length;
            if (Time_TextBlock != null)
                Time_TextBlock.Text = "(" + facade.GetViewLogic().MinutesToTimeText(Convert.ToInt32(validInteger)) + ")";
        }
    }
}
