using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            SelectedDirectories = new ObservableCollection<string>();
            SelectedDirectories.Add("C:\\");
            SelectedDirectories.Add("C:\\Temp\\");
            SelectedDirectories.Add(@"C:\Users\A539442\Pictures\From Le truc chiant\Camera Roll");
        }

        private ObservableCollection<string> selectedDirectories;
        public ObservableCollection<string> SelectedDirectories
        {
            get { return selectedDirectories; }
            set { selectedDirectories = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
