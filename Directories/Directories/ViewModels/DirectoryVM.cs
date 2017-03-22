using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;


namespace Directories.ViewModels
{
    public class DirectoryVM : INotifyPropertyChanged
    {
        public DirectoryVM()
        {
            _isChecked = false;
        }

        private DirectoryVM _parent;

        private string _name;
		public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string FullName { get; set; }

        private ObservableCollection<DirectoryVM> _directories;
		public ObservableCollection<DirectoryVM> Directories
        {
            get { return _directories; }
            set { _directories = value; OnPropertyChanged(); }
        }

        private bool? _isChecked;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                this.SetIsChecked(value, true, true);
            }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;
            OnPropertyChanged("IsChecked");

            if (updateChildren && value.HasValue)
            {
                foreach (var d in Directories)
                {
                    d.SetIsChecked(value, true, false);
                }
            }
            if (updateParent && _parent != null)
                _parent.VerifyCheckState();
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Directories.Count; ++i)
            {
                bool? current = this.Directories[i].IsChecked;
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

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public DirectoryVM Initialize(int level)
		{
            if (--level >= 0) ;
            {
                Directories = new ObservableCollection<DirectoryVM>();
                try
                {
                    var directories = System.IO.Directory.GetDirectories(FullName);
                    foreach (var d in directories)
                    {
                        var di = new DirectoryInfo(d);
                        if (di.Attributes.HasFlag(FileAttributes.System))
                            continue;
                        Directories.Add(new DirectoryVM() { _parent = this, FullName = d, Name = di.Name }.Initialize(level));
                    }
                }
                catch (Exception)
                {
                }
            }
			return this;
		}
	}
}
