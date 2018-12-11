using DatabaseManagement.Core;
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
    /// Interaction logic for Snapshot.xaml
    /// </summary>
    public partial class Snapshot : Window
    {
        private MainWindow _parent;
        public Snapshot(MainWindow parent)
        {
            _parent = parent;
            InitializeComponent();
            _parent.snapshots.ForEach(snapshot => SnapshotList.Items.Add(snapshot));
        }

        private void SnapshotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _parent.instance = (Main)e.AddedItems[0];
            _parent.Refresh_Button_Click(null, null);
        }
    }
}
