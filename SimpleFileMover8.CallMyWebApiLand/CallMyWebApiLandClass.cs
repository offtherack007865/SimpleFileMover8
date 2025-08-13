using log4net;
using Newtonsoft.Json;
using SimpleFileMover8.Data.Models;

namespace SimpleFileMover8.CallMyWebApiLand
{
    public class CallMyWebApiLandClass
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CallMyWebApiLandClass));

        public CallMyWebApiLandClass
        (
            string inputSimpleFileMover8BaseWebApiUrl
        )
        {
            MySimpleFileMover8BaseWebApiUrl = inputSimpleFileMover8BaseWebApiUrl;
        }
        public string MySimpleFileMover8BaseWebApiUrl { get; set; }

        public spGetMySimpleFileMover8ConfigOutput CallGetMySimpleFileMover8Config()
        {
            spGetMySimpleFileMover8ConfigOutput
                returnOutput =
                    CallGetMySimpleFileMover8ConfigAsync().Result;
        
            return returnOutput;
        }
        public async Task<spGetMySimpleFileMover8ConfigOutput> CallGetMySimpleFileMover8ConfigAsync()
        {
            spGetMySimpleFileMover8ConfigOutput
                returnOutput =
                    new spGetMySimpleFileMover8ConfigOutput();

            string myCompleteUrl = $"{MySimpleFileMover8BaseWebApiUrl}/api/Ops/GetMySimpleFileMover8Config";
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<spGetMySimpleFileMover8ConfigOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            if (returnOutput == null ||
                returnOutput.spGetMySimpleFileMover8ConfigOutputColumnsList.Count == 0)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = $"Url {myCompleteUrl} returned an error.";
                return returnOutput;
            }

            return returnOutput;
        }
    }
}