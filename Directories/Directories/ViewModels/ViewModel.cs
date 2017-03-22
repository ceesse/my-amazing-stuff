using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Directories.ViewModels
{
	public class ViewModel : INotifyPropertyChanged
	{
		public ViewModel()
		{
            
			//Initialize();
		}

        private ObservableCollection<DirectoryVM> _directories; 
		public ObservableCollection<DirectoryVM> Directories
		{
			get { return _directories; }
			set { _directories = value; OnPropertyChanged(); }
		}


        private void Initialize()
        {
            var drives = System.IO.Directory.GetLogicalDrives();
            Directories = new ObservableCollection<DirectoryVM>();

            foreach (var dr in drives)
            {
                var dri = new DriveInfo(dr);
                if (dri.DriveType.HasFlag(DriveType.Network))
                    continue;
                Directories.Add(new DirectoryVM() { FullName = dr, Name = dri.Name }.Initialize(2));
            }
        }
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
