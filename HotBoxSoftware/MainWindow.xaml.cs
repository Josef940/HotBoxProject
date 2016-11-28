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
        Hotbox hotbox = null;
        List<HotBoxValues> hotboxValues = null;
        readonly int ERRORSECONDS = 8;
        readonly int UPDATESECONDS = 15;

        public static DataGrid mDataGrid;
        public MainWindow()
        {
            InitializeComponent();
            hotbox = facade.GetDataBridge().GetHotBoxData();
            var modules = facade.GetDataLogic().GetModules(hotbox);
            hotboxValues = facade.GetDataLogic().GetHotBoxValues(hotbox);
            moduleDataGrid.ItemsSource = hotboxValues;
            mDataGrid = moduleDataGrid;
            Task.Factory.StartNew(()=>UpdateDataGrid());
            TextBlockError.Visibility = Visibility.Hidden;

        }

        public void UpdateDataGrid()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(UPDATESECONDS));
                var newHotboxValues = new List<HotBoxValues>();
                newHotboxValues = facade.GetDataLogic().UpdateHotBoxValues(ref hotbox);
                newHotboxValues = null;
                if (newHotboxValues != null)
                {
                    facade.GetDataLogic().SetValueDifference(ref hotboxValues, newHotboxValues);
                    InvokeDispatcher(() => moduleDataGrid.Items.Refresh());
                }
                else
                {
                    Task.Factory.StartNew(()=>TextBlockErrorToVisible());
                }
            }
        }

        // Sets the error message to visible for 'ERRORSECONDS' seconds
        public void TextBlockErrorToVisible()
        {
            InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Visible);
            Thread.Sleep(TimeSpan.FromSeconds(ERRORSECONDS));
            InvokeDispatcher(()=> TextBlockError.Visibility = Visibility.Hidden);
        }

        // Invokes the dispatcher object to access GUI elements from another thread than the main thread
        // Code should be passed to the parameter by using a lambda expression
        public void InvokeDispatcher(Action codetoexecute)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    codetoexecute();
                });
        }

        public void RefreshDataGrid(){
            moduleDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OpenValueWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vwin = new ValueWindow(moduleDataGrid.SelectedItem as HotBoxValues);
            vwin.Show();
        }

        // TEST STUFF


    }

}
