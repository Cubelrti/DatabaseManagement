using DatabaseManagement.Core;
using DatabaseManagement.Core.Entities;
using DatabaseManagement.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public List<Main> snapshots = new List<Main>();
        public Main instance;
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
            Refresh_Button_Click(null, null);
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
                instance.name = SQLCommand.Text;
                snapshots.Add((Main)instance.Clone());
            }
            catch (Exception ex)
            {
                Print("ERROR: " + ex.GetType());
            }
            Scroller.ScrollToEnd();
            SQLCommand.Text = "";
            Refresh_Button_Click(this, null);
        }

        public void SubmitSQLCommand(string sql)
        {
            SQLCommand.Text = sql;
        }

        private void RefreshDatabases()
        {
            // 获取所有的Database
            DatabaseList.Items.Clear();
            instance.databases.ForEach(db =>
            {
                var comboItem = new ComboBoxItem();
                comboItem.Content = db.name;
                DatabaseList.Items.Add(comboItem);
            });
        }

        private void RefreshTables()
        {
            // 获取当前的表
            if (instance._current == null)
            {
                return;
            }
            //SelectedDatabase.Content = instance._current.name;
            TableList.ItemsSource = new ObservableCollection<Core.Entities.Table>(instance._current.tables);
            TableList.UnselectAll();
        }

        public void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshDatabases();
            RefreshTables();
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreateDatabase create = new CreateDatabase(this);
            create.Show();
        }

        private void DatabaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
            {
                return;
            }
            ComboBoxItem item = (ComboBoxItem)e.AddedItems[0];
            instance.SelectDatabase((string)item.Content);
            RefreshTables();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DropDatabase drop = new DropDatabase(this);
            drop.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CreateTable create = new CreateTable(this);
            create.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            DropTable drop = new DropTable(this);
            drop.Show();
        }
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(RowList.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        private void RowList_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "TiDatabase(*.db)|*.db|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                IO.Serialize(instance, dialog.FileName);
                Print($"--- Saved Snapshot to {dialog.FileName} ---");
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "TiDatabase(*.db)|*.db|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                IO.Deserialize(instance, dialog.FileName);
                Print($"--- Read snapshot from {dialog.FileName} ---");
            }
            Refresh_Button_Click(null, null);
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Snapshot snapshot = new Snapshot(this);
            snapshot.Show();
        }
    }
}
