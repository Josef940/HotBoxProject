﻿using System;
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

        }

        public void UpdateDataGrid()
        {
            while (true)
            {
                Thread.Sleep(15000);
                var newHotboxValues = new List<HotBoxValues>();
                newHotboxValues = facade.GetDataLogic().AutoUpdateHotBoxValues(5);
                if (newHotboxValues != null)
                {
                    facade.GetDataLogic().SetValueDifference(ref hotboxValues, newHotboxValues);
                }
                MessageBox.Show("Values updated");
            }
        }

        public void RefreshDataGrid(){
            moduleDataGrid.Items.Refresh();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            moduleDataGrid.Items.Refresh();
        }
    }

}
