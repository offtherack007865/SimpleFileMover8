using SimpleFileMover8.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.Data.Models
{
    public class MoveFilesSingleFileTypeProcessingOptions
    {
        public MoveFilesSingleFileTypeProcessingOptions(ConfigOptions inputConfigOptions, spGetMySimpleFileMover8ConfigOutputColumns inputSpGetMySimpleFileMover8ConfigOutputColumns)
        {
            MyConfigOptions = inputConfigOptions;
            this.mySpGetMySimpleFileMover8ConfigOutputColumns = inputSpGetMySimpleFileMover8ConfigOutputColumns;
        }

        public ConfigOptions MyConfigOptions { get; set; }
        public spGetMySimpleFileMover8ConfigOutputColumns mySpGetMySimpleFileMover8ConfigOutputColumns { get; set; }

    }
}
