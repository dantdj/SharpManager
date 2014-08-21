using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpManager.Helpers
{
    class FileProperties
    {
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public string Filesize { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }

        public FileProperties(string filename, string filetype, string filesize, string dateCreated, string dateModified)
        {
            Filename = filename;
            Filetype = filetype;
            Filesize = filesize;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
    }
}
