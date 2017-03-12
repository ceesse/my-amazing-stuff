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

namespace Directories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModels.ViewModel vm = new ViewModels.ViewModel();
            treeView.ItemsSource = vm.Directories;
        }

        private void OnTreeViewItemChecked(object sender, RoutedEventArgs e)
        {
            TreeViewItem it = ((TreeViewItem)sender);
        }

        private void OnTreeViewItemUnchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
