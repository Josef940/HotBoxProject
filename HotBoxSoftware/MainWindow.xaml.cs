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
        readonly int UPDATESECONDS = 5;
        readonly int ERRORMESSAGESECONDS = 12;
        public MainWindow()
        {
            InitializeComponent();
            //TextBlockError.Visibility = Visibility.Hidden;
            moduleDataGrid.ItemsSource = hotboxValues;
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            //if (hotboxValues == null || writeablehotboxValues == null)
            //    Task.Factory.StartNew(() => TextBlockErrorToVisible());
            //Task.Factory.StartNew(() => UpdateDataGrid());
            //Task.Factory.StartNew(()=>UpdateDataGridLoop());
            UpdateDataGridLoop();
        }
        private Task TEST()
        {
            TextBlockLoading.Visibility = Visibility.Visible;
            return Task.Factory.StartNew(() => UpdateDataGridLoopTEST());
        }

        private async void UpdateDataGridLoopTEST()
        {
            await Task.Factory.StartNew(() => facade.GetDataLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues))
                .ContinueWith(r =>
                {
                    TextBlockLoading.Visibility = Visibility.Hidden;
                    if (r.Result)
                        RefreshHotBoxDataGrid();
                    else
                        Task.Factory.StartNew(() => TextBlockErrorToVisible());

                }, TaskScheduler.FromCurrentSynchronizationContext());
            Thread.Sleep(TimeSpan.FromSeconds(UPDATESECONDS));

        }
        private void UpdateDataGridLoop()
        {
            Task.Factory.StartNew(()=>
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

        private void UpdateDataGridLoopd()
        {
            TextBlockLoading.Visibility = Visibility.Visible;
            Task.Factory.StartNew(() => facade.GetDataLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues))
                .ContinueWith(r =>
                {
                    TextBlockLoading.Visibility = Visibility.Hidden;
                    if (r.Result)
                        RefreshHotBoxDataGrid();
                    else
                        TextBlockErrorToVisible();

                }, TaskScheduler.FromCurrentSynchronizationContext());
            //Thread.Sleep(TimeSpan.FromSeconds(UPDATESECONDS));
        }

        // Starts new thread and
        // sets the error message to visible for ERRORMESSAGESECONDS seconds
        public Task TextBlockErrorToVisible()
        {
            return Task.Factory.StartNew(() =>
            {
                InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Visible);
                Thread.Sleep(TimeSpan.FromSeconds(ERRORMESSAGESECONDS));
            })
                .ContinueWith(_ => InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Hidden));
        }

        public void RefreshHotBoxDataGrid()
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

        public void RefreshDataGrid()
        {
            moduleDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OpenValueWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vwin = new ValueWindow(DataGridwriteableModule.SelectedItem as HotBoxValues);
            vwin.Show();
        }
    }

}
