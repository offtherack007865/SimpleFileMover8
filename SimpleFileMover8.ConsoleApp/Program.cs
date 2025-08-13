using log4net;
using Microsoft.Extensions.Configuration;
using SimpleFileMover8.CallMyWebApiLand;
using SimpleFileMover8.Data;
using SimpleFileMover8.Data.Models;
using System.Diagnostics;
namespace SimpleFileMover8.ConsoleApp1
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // Limit Program to run one instance only.
        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }
        public static void Main(string[] args)
        {
            if (PriorProcess() != null)
            {

                log.Error("Another instance of the app is already running.");
                return;
            }

            // configure logging via log4net
            string log4netConfigFullFilename =
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "log4net.config");
            var fileInfo = new FileInfo(log4netConfigFullFilename);
            if (fileInfo.Exists)
                log4net.Config.XmlConfigurator.Configure(fileInfo);
            else
                throw new InvalidOperationException("No log config file found");


            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(SimpleFileMover8.Data.MyConstants.AppSettingsFile)
                .Build();

            // Read in Configuration Options for this Console Application
            ReadInConfigOptions myReadInConfigOptions = new ReadInConfigOptions(config);
            SimpleFileMover8.Data.Models.ConfigOptions
                myConfigOptions =
                    myReadInConfigOptions.ReadIn();

            // Get Config Options from the database.
            CallMyWebApiLandClass
                myCallForGetOptions =
                    new CallMyWebApiLandClass
                        (
                            myConfigOptions.SimpleFileMover8WebApiUrl
                        );

            spGetMySimpleFileMover8ConfigOutput
                mySpGetMySimpleFileMover8ConfigOutput =
                    myCallForGetOptions.CallGetMySimpleFileMover8Config();
            if (!mySpGetMySimpleFileMover8ConfigOutput.IsOk ||
                mySpGetMySimpleFileMover8ConfigOutput.spGetMySimpleFileMover8ConfigOutputColumnsList.Count == 0)
            {
                log.Error($"We had an error in trying to get the configuration file from the database:  {mySpGetMySimpleFileMover8ConfigOutput.ErrorMessage}");
                return;
            }
            MoveFilesMainProcessingConfig
                myMoveFilesMainProcessingConfig =
                    new MoveFilesMainProcessingConfig
                        (
                            mySpGetMySimpleFileMover8ConfigOutput.spGetMySimpleFileMover8ConfigOutputColumnsList
                            , myConfigOptions
                        );

            // Main Operations.
            MainOps
                myMainOps =
                    new MainOps
                        (
                            myMoveFilesMainProcessingConfig
                        );

            MyMainOutput
                myMainOutput =
                    myMainOps.MainProcessing();
            if (!myMainOutput.IsOk)
            {
                log.Error(myMainOutput.ErrorMessage);
                return;
            }
        }
    }
}
