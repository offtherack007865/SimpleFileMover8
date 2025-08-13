using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleFileMover8.Data.Models;

namespace SimpleFileMover8.WebApiLand.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpsController));

        public OpsController(SimpleFileMover8Context inputMySftpContext)
        {
            MyContext = inputMySftpContext;

            log.Info($"Start of OpsController Connection String:  {MyContext.MyConnectionString}");

        }
        public SimpleFileMover8Context MyContext { get; set; }


        // GET /api/Ops/GetMySimpleFileMover8Config 
        [HttpGet]
        public spGetMySimpleFileMover8ConfigOutput GetMySimpleFileMover8Config()
        {
             spGetMySimpleFileMover8ConfigOutput returnOutput =
                new spGetMySimpleFileMover8ConfigOutput();
            string sql = $"spGetMySimpleFileMover8Config";

            List<SqlParameter> parms = new List<SqlParameter>();
            try
            {
                returnOutput.spGetMySimpleFileMover8ConfigOutputColumnsList =
                    MyContext
                    .spGetMySimpleFileMover8ConfigOutputColumnsList
                    .FromSqlRaw<spGetMySimpleFileMover8ConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }


            return returnOutput;
        }
    }
}
