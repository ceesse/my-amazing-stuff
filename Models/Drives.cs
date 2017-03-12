using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories.Models
{
	public class Drives
	{
		public List<Directory> Directories { get; set; }

		public void Initialize()
		{
			var drives = System.IO.Directory.GetLogicalDrives();
            Directories = new List<Directory>();

			foreach (var dr in drives)
			{
                System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dr);
                if (info.Exists)
                {
                    Directories.Add(new Directory() { Name = dr }.Initialize());
                }
			}
		}
	}
}
