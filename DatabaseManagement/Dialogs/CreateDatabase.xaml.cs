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

namespace DatabaseManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class CreateDatabase : Window
    {
        private MainWindow mainWindow;
        public CreateDatabase(MainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SubmitSQLCommand($"CREATE DATABASE {dbName.Text};");
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
