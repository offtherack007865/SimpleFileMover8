using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class MyMainOutput
    {
        public MyMainOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
    }
}
