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
        readonly int UPDATESECONDS = 15;
        readonly int ERRORMESSAGESECONDS = 12;
        public MainWindow()
        {
            InitializeComponent();
            //new ValueChartWindow("P2",300).Show();
            moduleDataGrid.ItemsSource = hotboxValues;
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            UpdateDataGridLoop();
        }

        private Task UpdateDataGridLoop()
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    InvokeDispatcher(() => TextBlockLoading.Visibility = Visibility.Visible);
                    bool updated = facade.GetDataLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues);
                    InvokeDispatcher(() => TextBlockLoading.Visibility = Visibility.Hidden);

                    if (updated)
                        InvokeDispatcher(() => RefreshHotBoxDataGrid());
                    else
                        Task.Factory.StartNew(() => TextBlockErrorToVisible());
                    Thread.Sleep(TimeSpan.FromSeconds(UPDATESECONDS));
                }
            });
        }

        // Starts new thread and
        // sets the error message to visible for ERRORMESSAGESECONDS seconds
        private Task TextBlockErrorToVisible()
        {
            return Task.Factory.StartNew(() =>
            {
                InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Visible);
                Thread.Sleep(TimeSpan.FromSeconds(ERRORMESSAGESECONDS));
            })
                .ContinueWith(_ => InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Hidden));
        }

        private void RefreshHotBoxDataGrid()
        {
            moduleDataGrid.ItemsSource = hotboxValues;
            moduleDataGrid.Items.Refresh();
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            DataGridwriteableModule.Items.Refresh();
            DataGridwriteableModule.Columns.RemoveAt(4);
        }
        // Invokes the dispatcher object to access GUI elements from another thread than the main thread
        // Code should be passed to the parameter by using a lambda expression
        // CODE REFACTORED, THIS METHOD MAY BE REDUNDANT
        public void InvokeDispatcher(Action codetoexecute)
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

        private void TEST_Click(object sender, RoutedEventArgs e)
        {
            var test = facade.GetDBLogic().GetPointValuesForChart("P2", 180);
        }

        private void Odpen_PickChartInfoWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var chartinfowin = new PickChartInfoWindow(DataGridwriteableModule.SelectedItem as HotBoxValues);
            chartinfowin.Show();

        }

        private void Open_ValueChartWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int minutes = Convert.ToInt32(Minutes_TextBox.Text);
            var chartwin = new ValueChartWindow(DataGridwriteableModule.SelectedItem as HotBoxValues, minutes);
            chartwin.Show();
        }
        private void Second_Open_ValueChartWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int minutes = Convert.ToInt32(Minutes_TextBox.Text);
            var chartwin = new ValueChartWindow(moduleDataGrid.SelectedItem as HotBoxValues, minutes);
            chartwin.Show();
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
            Minutes_TextBox.Text = facade.GetViewLogic().GetValidInteger(Minutes_TextBox.Text,6);
            Minutes_TextBox.SelectionStart = Minutes_TextBox.Text.Length;
        }
    }
}
