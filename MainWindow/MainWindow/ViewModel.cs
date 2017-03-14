using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            Files = new ObservableCollection<string>();
            SelectedDirectories.Add("C:\\");
            SelectedDirectories.Add("C:\\Temp\\");
            SelectedDirectories.Add(@"C:\Users\A539442\Pictures\From Le truc chiant\Camera Roll");
        }

        private ObservableCollection<string> _selectedDirectories;
        public ObservableCollection<string> SelectedDirectories
        {
            get { return _selectedDirectories; }
            set { _selectedDirectories = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _files;
        public ObservableCollection<string> Files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(); }
        }

        private string _selectedDirectory;
        public string SelectedDirectory
        {
            get { return _selectedDirectory; }
            set { _selectedDirectory = value; UpdateFiles(); OnPropertyChanged(); }
        }

        private void UpdateFiles()
        {
            Files.Clear();
            var files = Directory.EnumerateFiles(_selectedDirectory);
            foreach (var f in files)
            {
                var fi = new FileInfo(f);
                //Files.Add(fi.Name);
                Files.Add(f);
            }
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
