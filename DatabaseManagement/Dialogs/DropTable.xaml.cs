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
    /// Interaction logic for DropTable.xaml
    /// </summary>
    public partial class DropTable : Window
    {
        private MainWindow mainWindow;
        public DropTable(MainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SubmitSQLCommand($"DROP TABLE {dbName.Text};");
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
