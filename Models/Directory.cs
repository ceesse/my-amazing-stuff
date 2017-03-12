using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Models
{
    public class Directory
    {
		public string Name { get; set; }

		public List<Directory> Directories { get; set; }

		public bool Selected { get; set; }

		public Directory Initialize()
		{
            try
            {
			    var directories = System.IO.Directory.GetDirectories(Name);
			    Directories = new List<Directory>();
			    foreach (var d in directories)
			    {
                    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(d);
                    if (info.Attributes.HasFlag(System.IO.FileAttributes.Hidden) || info.Attributes.HasFlag(System.IO.FileAttributes.System))
                        continue;
				    Directories.Add(new Directory() { Name = d }.Initialize());
			    }
            }
            catch (Exception)
            {
            }

            return this;
		}
	}
}
