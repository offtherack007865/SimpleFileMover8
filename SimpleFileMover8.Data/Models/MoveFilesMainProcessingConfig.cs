using SimpleFileMover8.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class MoveFilesMainProcessingConfig
    {
        public MoveFilesMainProcessingConfig(List<spGetMySimpleFileMover8ConfigOutputColumns> inputSpGetMySimpleFileMover8ConfigOutputColumnsList, ConfigOptions inputConfigOptions)
        {
            MySpGetMySimpleFileMover8ConfigOutputColumnsList = inputSpGetMySimpleFileMover8ConfigOutputColumnsList;
            MyConfigOptions = inputConfigOptions;
        }

        public List<spGetMySimpleFileMover8ConfigOutputColumns> MySpGetMySimpleFileMover8ConfigOutputColumnsList { get; set; }
        public ConfigOptions MyConfigOptions { get; set; }
    }
}
