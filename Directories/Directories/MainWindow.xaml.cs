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
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        }

        private void Populate(string header, string tag, TreeView root, TreeViewItem child, bool hasSubdirs)
        {
            TreeViewItem item = new TreeViewItem();
            item.Tag = new TreeItemTag() { Owner = item, IsChecked = true, FullName = tag };
            item.Header = header;
            item.Expanded += new RoutedEventHandler(ItemExpanded);
            if (hasSubdirs)
            {
                item.Items.Add(new TreeViewItem());
            }

            if (root != null)
            {
                root.Items.Add(item);
            }
            else
            {
                child.Items.Add(item);
            }
        }

        void ItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && ((TreeViewItem)item.Items[0]).Header == null)
            {
                item.Items.Clear();
                foreach (string dir in Directory.GetDirectories(((TreeItemTag)item.Tag).FullName))
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    if (di.Attributes.HasFlag(FileAttributes.System))
                        continue;
                    try
                    {
                        var sd = Directory.GetDirectories(di.FullName);
                        Populate(di.Name, di.FullName, null, item, sd.Count() > 0);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }
        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnOKButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (DriveInfo driv in DriveInfo.GetDrives())
            {
                if (driv.IsReady)
                {
                    var sd = Directory.GetDirectories(driv.Name);
                    Populate(driv.Name, driv.Name, foldersTree, null, sd.Count() > 0);
                }
            }
        }
    }

    public class TreeItemTag : INotifyPropertyChanged
    {
        private bool? _isChecked;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        public string FullName { get; set; }
        public TreeViewItem Owner { get; set; }

        public void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;
            OnPropertyChanged("IsChecked");

            if (updateChildren && value.HasValue)
            {
                foreach (var item in Owner.Items)
                {
                    TreeItemTag tag = (TreeItemTag)((TreeViewItem)item).Tag;
                    if (tag != null)
                    {
                        tag.SetIsChecked(value, true, false);
                    }
                }
            }
            if (updateParent && Owner.Parent != null)
            {
                if (Owner.Parent is TreeViewItem)
                {
                    TreeItemTag tag = (TreeItemTag)((TreeViewItem)Owner.Parent).Tag;
                    if (tag != null)
                    {
                        tag.VerifyCheckState();
                    }
                }
            }
        }

        public void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < Owner.Items.Count; ++i)
            {
                bool? current = ((TreeItemTag)((TreeViewItem)Owner.Items[i]).Tag).IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }


        #region Notification

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
