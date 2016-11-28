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
            facade.GetDataLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues);
            moduleDataGrid.ItemsSource = hotboxValues;
            DataGridwriteableModule.ItemsSource = writeablehotboxValues;
            if(hotboxValues==null||writeablehotboxValues==null)
                Task.Factory.StartNew(() => TextBlockErrorToVisible());
            Task.Factory.StartNew(()=>UpdateDataGrid());
            TextBlockError.Visibility = Visibility.Hidden;
        }

        public void UpdateDataGrid()
        {
            if(hotboxValues != null && writeablehotboxValues != null)
                InvokeDispatcher(()=> DataGridwriteableModule.Columns.RemoveAt(4));
            while (true)
            {
                
                Thread.Sleep(TimeSpan.FromSeconds(UPDATESECONDS));
                if (facade.GetDataLogic().UpdateHotBoxValues(ref hotboxValues, ref writeablehotboxValues))
                {
                    InvokeDispatcher(() => {
                        moduleDataGrid.ItemsSource = hotboxValues;
                        moduleDataGrid.Items.Refresh();
                        DataGridwriteableModule.ItemsSource = writeablehotboxValues;
                        DataGridwriteableModule.Items.Refresh();
                        DataGridwriteableModule.Columns.RemoveAt(4);
                    });
                    
                }
                else
                {
                    Task.Factory.StartNew(()=>TextBlockErrorToVisible());
                }
            }
        }

        // Sets the error message to visible until next update
        public void TextBlockErrorToVisible()
        {
            InvokeDispatcher(() => TextBlockError.Visibility = Visibility.Visible);
            Thread.Sleep(TimeSpan.FromSeconds(ERRORMESSAGESECONDS));
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
            var vwin = new ValueWindow(DataGridwriteableModule.SelectedItem as HotBoxValues);
            vwin.Show();
        }
    }

}
