using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class spGetMySimpleFileMover8ConfigOutputColumns
    {
        public string SystemName { get; set; }
        public bool Enabled { get; set; }
        public string SourceDirectory { get; set; }
        public bool SearchRootSourceDirectory { get; set; }
        public bool SearchSubdirectoriesOfRootSourceDirectory { get; set; }
        public string RequiredFilePrefix { get; set; }
        public bool DeleteSourceFile { get; set; }
        public string DestDirs { get; set; }
        public string Emailees { get; set; }

    }
}
