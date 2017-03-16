using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            SelectedDirectories = new ObservableCollection<string>();
            Files = new ObservableCollection<string>();
            Filter = Properties.Settings.Default.ExtensionFilter;
            SelectedDirectories.Add("C:\\");
            SelectedDirectories.Add("C:\\Temp\\");
            SelectedDirectories.Add(@"C:\Users\A539442\Pictures\From Le truc chiant\Camera Roll");
        }

        private List<string> _validExtensions;
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
                if (FilterFile(f))
                    Files.Add(f);
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set { _filter = value; CreateValidExtensions(); OnPropertyChanged(); }
        }

        private void CreateValidExtensions()
        {
            _validExtensions = Filter.Split(new char[] { ',', ';', '|' }).ToList();
        }

        protected bool FilterFile(string name)
        {
            var fi = new FileInfo(name);
            var ext = fi.Extension;
            return _validExtensions.FindAll(s => s.Equals(ext)).Count() > 0;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
