using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Directories.Models;
using System.Collections.ObjectModel;

namespace Directories.ViewModels
{
	public class ViewModel : BaseVM
	{
		public ViewModel()
		{
            Drives drives = new Drives();
			drives.Initialize();
            Directories = new ObservableCollection<DirectoryVM>();
            foreach (var d in drives.Directories)
            {
                Directories.Add(new DirectoryVM(d));
            }
		}
		private ObservableCollection<DirectoryVM> drives; 
		public ObservableCollection<DirectoryVM> Directories
		{
			get { return drives; }
			set { drives = value; OnPropertyChanged(); }
		}
		
	}
}
