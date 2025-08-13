using SimpleFileMover8.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class spGetMySimpleFileMover8ConfigOutput
    {
        public spGetMySimpleFileMover8ConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            spGetMySimpleFileMover8ConfigOutputColumnsList =
                new List<spGetMySimpleFileMover8ConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<spGetMySimpleFileMover8ConfigOutputColumns> spGetMySimpleFileMover8ConfigOutputColumnsList { get; set; }
    }
}
