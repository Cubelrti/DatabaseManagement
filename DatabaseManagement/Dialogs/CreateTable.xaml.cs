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
using System.Windows.Shapes;

namespace DatabaseManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateTable.xaml
    /// </summary>
    public partial class CreateTable : Window
    {
        private MainWindow mainWindow;
        public ObservableCollection<Column> columns;
        public CreateTable(MainWindow parent)
        {

            InitializeComponent();
            DataContext = this;
            mainWindow = parent;
            columns = new ObservableCollection<Column>();
        }
        public ObservableCollection<Column> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var types = columns.Select(col => col.Name + " " + col.Type).Aggregate((a, b) => a + ", " + b);
            mainWindow.SubmitSQLCommand($"CREATE TABLE {dbName.Text} ({types});");
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    public class Column
    {
        public ObservableCollection<string> Types { get; } = new ObservableCollection<string>();
        public string Name { get; set; }
        public string Type { get; set; }
        public Column()
        {
            Types.Add("VARCHAR");
            Types.Add("INTEGER");
            Types.Add("DATE");
            Types.Add("DOUBLE");
        }
    }

}
