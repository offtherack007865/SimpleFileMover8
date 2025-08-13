using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class ConfigOptions
    {
        public ConfigOptions()
        {
            SimpleFileMover8WebApiUrl = string.Empty;
            EmailFromAddress = string.Empty;
            EmailWebApiUrl = string.Empty;
        }
        public string SimpleFileMover8WebApiUrl { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailWebApiUrl { get; set; }
    }
}
