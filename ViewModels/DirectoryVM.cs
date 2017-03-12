using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directories.Models;

namespace Directories.ViewModels
{
    public class DirectoryVM : BaseVM
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<DirectoryVM> Directories { get; set; }

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }


        public DirectoryVM()
        {
        }

        public DirectoryVM(Directory d)
        {
            Name = d.Name;
            Selected = d.Selected;
            Selected = true;
            if (d.Directories != null)
            {
                Directories = new ObservableCollection<DirectoryVM>();
                foreach (var dir in d.Directories)
                {
                    Directories.Add(new DirectoryVM(dir));
                }
            }
        }
    }
}
