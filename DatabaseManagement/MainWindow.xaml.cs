using DatabaseManagement.Core;
using DatabaseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DatabaseManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Main instance;
        private Executor executor;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            runSQLCommand();
        }
        private void Print(string log)
        {
            Log.Text += '\n' + log;
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Text = "TiSQL Builder\n2018 Cubelrti All rights reserved.";
            instance = new Main();
            Print("Loaded Core: NaiveSQL v0.1");
            executor = new Executor(instance);
            Print("Loaded Executor: Tinterpreter v0.1");
            Print("");
            IO.Deserialize(instance);
            Print($"Loaded {instance.databases.Count} database(s).");
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                runSQLCommand();
            }
        }

        private void runSQLCommand()
        {
            Print("SQL > " + SQLCommand.Text);
            try
            {
                var result = executor.Run(SQLCommand.Text)
                    .Split('\n')
                    .Select(line => "      " + line)
                    .Aggregate((a, b) => a + '\n' + b);
                Print(result);
            }
            catch (Exception ex)
            {
                Print("ERROR: " + ex.GetType());
            }
            Scroller.ScrollToEnd();
            SQLCommand.Text = "";
            Refresh_Button_Click(this, null);
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            if (instance._current == null)
            {
                return;
            }
            SelectedDatabase.Content = instance._current.name;
            TableList.ItemsSource = new ObservableCollection<Core.Entities.Table>(instance._current.tables);
            TableList.UnselectAll();
        }

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
            {
                return;
            }
            var item = (Core.Entities.Table)e.AddedItems[0];
            RowGridView.AllowsColumnReorder = true;
            RowGridView.Columns.Clear();
            RowList.DataContext = item.rows;
            item.ColumnDefinitions.Select(kv =>
            {
                var column = new GridViewColumn();
                column.Header = kv.Key;
                column.DisplayMemberBinding = new Binding($"data[{kv.Key}]");
                return column;
            }).ToList().ForEach(col => RowGridView.Columns.Add(col));

            RowList.ItemsSource = new ObservableCollection<Row>(item.rows);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IO.Serialize(instance);
        }
    }
}
