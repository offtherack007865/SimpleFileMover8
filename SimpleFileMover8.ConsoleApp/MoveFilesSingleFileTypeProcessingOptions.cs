using EmailWebApiLand8.CallEmailWebApiLand8;
using EmailWebApiLand8.data;
using EmailWebApiLand8.data.Models;
using log4net;
using SimpleFileMover8.ConsoleApp1;
using SimpleFileMover8.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SimpleFileMover8.ConsoleApp1
{
    public class MoveFilesSingleFileTypeProcessing
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public MoveFilesSingleFileTypeProcessingOptions MyMoveFilesSingleFileTypeProcessingOptions { get; set; }

        public MoveFilesSingleFileTypeProcessing(MoveFilesSingleFileTypeProcessingOptions inputMoveFilesSingleFileTypeProcessingOptions)
        {
            MyMoveFilesSingleFileTypeProcessingOptions = inputMoveFilesSingleFileTypeProcessingOptions;
        }
        public void MoveSingleFileType()
        {

            List<string> DestDirList = new List<string>();
            List<string> EmaileeList = new List<string>();
            string EmailSubjectLine = string.Empty;
            List<string> EmailBodyLines = new List<string>();

            DirectoryInfo d =
                new
                    DirectoryInfo
                    (
                        MyMoveFilesSingleFileTypeProcessingOptions
                        .mySpGetMySimpleFileMover8ConfigOutputColumns
                        .SourceDirectory
                    );

            // To account for Covenant having a static suffix instead of a static prefix in its filenames, get all files matching both criteria.
            FileInfo[]
                prefixedFiles =
                    d.GetFiles
                    (
                        $"{MyMoveFilesSingleFileTypeProcessingOptions
                            .mySpGetMySimpleFileMover8ConfigOutputColumns
                            .RequiredFilePrefix}*.*");
            FileInfo[]
                suffixedFiles =
                    d.GetFiles($"*{MyMoveFilesSingleFileTypeProcessingOptions
                        .mySpGetMySimpleFileMover8ConfigOutputColumns
                        .RequiredFilePrefix}");


            List<FileInfo> Files = new List<FileInfo>();
            // Add prefixed files found to total files found
            foreach (var loopPrefixedFi in prefixedFiles)
            {
                Files.Add(loopPrefixedFi);
            }

            // Add suffixed files found to total files found
            foreach (var loopSuffixedFi in suffixedFiles)
            {
                Files.Add(loopSuffixedFi);
            }


            if (Files.Count > 0)
            {
                string[]
                    destDirPartsArray =
                        MyMoveFilesSingleFileTypeProcessingOptions
                        .mySpGetMySimpleFileMover8ConfigOutputColumns
                        .DestDirs
                        .Split(";");
                if (destDirPartsArray.Length > 0)
                {
                    foreach (string destDirPart in destDirPartsArray)
                    {
                        DestDirList.Add(destDirPart.Trim());
                    }
                }

                string[]
                    emaileesPartsArray =
                        MyMoveFilesSingleFileTypeProcessingOptions
                        .mySpGetMySimpleFileMover8ConfigOutputColumns
                        .Emailees
                        .Split(";");
                if (emaileesPartsArray.Length > 0)
                {
                    foreach (string emaileesPart in emaileesPartsArray)
                    {
                        EmaileeList.Add(emaileesPart.Trim());
                    }
                }

                EmailSubjectLine = $"The {MyMoveFilesSingleFileTypeProcessingOptions.mySpGetMySimpleFileMover8ConfigOutputColumns.SystemName} system had {Files.Count} file(s) moved to {DestDirList.Count} destination locations..";

                foreach (FileInfo loopFi in Files)
                {
                    for (int destCtr = 0; destCtr < DestDirList.Count; destCtr++)
                    {
                        string loopDestDir = DestDirList[destCtr].ToString();
                        string destFullFilename = Path.Combine(loopDestDir, loopFi.Name);
                        if (System.IO.File.Exists(destFullFilename))
                        {
                            System.IO.File.Delete(destFullFilename);
                        }
                            if (MyMoveFilesSingleFileTypeProcessingOptions.mySpGetMySimpleFileMover8ConfigOutputColumns.DeleteSourceFile && destCtr == (DestDirList.Count - 1))
                        {
                            System.IO.File.Move(loopFi.FullName, destFullFilename);
                        }
                        else
                        {
                            System.IO.File.Copy(loopFi.FullName, destFullFilename);
                        }

                        // Break up the Source path into directory names.
                        List<string>
                            rawSourceDirectoryNameList =
                                loopFi.DirectoryName.Split("\\").ToList();

                        List<string>
                            finishedSourceDirectoryNameList = new List<string>();

                        foreach (string loopDirectoryName in rawSourceDirectoryNameList)
                        {
                            if (!loopDirectoryName.Contains("\\") &&
                                loopDirectoryName.Trim().Length > 0)
                            {
                                finishedSourceDirectoryNameList.Add(loopDirectoryName.Trim());
                            }
                        }

                        // Break up the Destination path into directory names.
                        List<string>
                            rawDestinationDirectoryNameList =
                                loopDestDir.Split("\\").ToList();

                        List<string>
                            finishedDestinationDirectoryNameList = new List<string>();

                        foreach (string loopDirectoryName in rawDestinationDirectoryNameList)
                        {
                            if (!loopDirectoryName.Contains("\\") &&
                                loopDirectoryName.Trim().Length > 0)
                            {
                                finishedDestinationDirectoryNameList.Add(loopDirectoryName.Trim());
                            }
                        }

                        EmailBodyLines.Add($"<b>Filename: {loopFi.Name}</b><br><br>moved from<br><br>Source Directory<ul>");
                        foreach (string loopDirectoryName in finishedSourceDirectoryNameList)
                        {
                            EmailBodyLines.Add($"<li>{loopDirectoryName}</li>");
                        }

                        EmailBodyLines.Add($"</ul><br>To<br><br>Destination Directory<ul>");

                        foreach (string loopDirectoryName in finishedDestinationDirectoryNameList)
                        {
                            EmailBodyLines.Add($"<li>{loopDirectoryName}</li>");
                        }
                        EmailBodyLines.Add($"</ul>");
                    }
                }
                if (EmailBodyLines.Count > 0)
                {
                    string mySubjectLine = EmailSubjectLine;
                    string myBody = string.Join("", EmailBodyLines);   //myMainOutput.EmailBody;

                    string myFromEmailAddress =
                        MyMoveFilesSingleFileTypeProcessingOptions
                        .MyConfigOptions
                        .EmailFromAddress;

                    string myEmailWebApiDotNet8BaseUrl =
                        MyMoveFilesSingleFileTypeProcessingOptions
                        .MyConfigOptions
                        .EmailWebApiUrl;

                    // Email the notifyees.
                    CallEmailWebApiLand8 myCallEmailWebApi =
                        new CallEmailWebApiLand8
                        (
                            mySubjectLine // string inputEemailSubject
                            , myBody // List<string> inputEmailBodyLineList
                            , EmaileeList // List<string> inputEmailAddressList
                            , myFromEmailAddress // string inputFromEmailAddress
                            , myEmailWebApiDotNet8BaseUrl // string inputEmailWebApiBaseUrl
                            , new List<string>()
                        );
                    EmailSendWithHtmlStringOutput
                        myEmailSendWithHtmlStringOutput =
                            myCallEmailWebApi.CallIHtmlStringBody();
                    if (!myEmailSendWithHtmlStringOutput.IsOk)
                    {
                        string errorMessage =
                            $"Error upon trying to invoke the Email Web Api with Url:  {myEmailWebApiDotNet8BaseUrl} and subject line of {mySubjectLine}";
                        log.Error(errorMessage);

                        return;
                    }
                }
            }
        }
    }
}
