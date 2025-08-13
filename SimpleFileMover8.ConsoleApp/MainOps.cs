using log4net;

using SimpleFileMover8.Data.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.ConsoleApp1
{
    public class MainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public MainOps(MoveFilesMainProcessingConfig inputMoveFilesMainProcessingConfig)
        {
            MyMoveFilesMainProcessingConfig = inputMoveFilesMainProcessingConfig;
        }
        
        public MoveFilesMainProcessingConfig MyMoveFilesMainProcessingConfig { get; set; }

        public MyMainOutput MainProcessing()
        {
            MyMainOutput returnOutput = new MyMainOutput();
            foreach (spGetMySimpleFileMover8ConfigOutputColumns configLoop in MyMoveFilesMainProcessingConfig.MySpGetMySimpleFileMover8ConfigOutputColumnsList)
            {
                MoveFilesSingleFileTypeProcessingOptions myMoveFilesSingleFileTypeProcessingOptions =
                    new MoveFilesSingleFileTypeProcessingOptions(MyMoveFilesMainProcessingConfig.MyConfigOptions, configLoop);
                MoveFilesSingleFileTypeProcessing mySingleFileProcessing =
                    new MoveFilesSingleFileTypeProcessing
                        (
                            myMoveFilesSingleFileTypeProcessingOptions
                        );
                mySingleFileProcessing.MoveSingleFileType();
                    log.Info("Completed moving files for: " + configLoop.SystemName);
                }
            return returnOutput;
        }

    }
}
