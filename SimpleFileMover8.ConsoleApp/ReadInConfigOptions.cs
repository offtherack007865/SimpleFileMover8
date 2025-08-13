using Microsoft.Extensions.Configuration;
using SimpleFileMover8.Data;
using SimpleFileMover8.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileMover8.ConsoleApp1
{
    public class ReadInConfigOptions
    {
        public ReadInConfigOptions(IConfiguration myConfig)
        {
            MyConfig = myConfig;
        }

        public IConfiguration MyConfig { get; set; }

        public ConfigOptions ReadIn()
        {
            ConfigOptions returnConfigOptions = new ConfigOptions();

            returnConfigOptions.SimpleFileMover8WebApiUrl = MyConfig.GetValue<string>(MyConstants.SimpleFileMover8WebApiUrl);
            returnConfigOptions.EmailFromAddress = MyConfig.GetValue<string>(MyConstants.EmailFromAddress);
            returnConfigOptions.EmailWebApiUrl = MyConfig.GetValue<string>(MyConstants.EmailWebApiUrl);

            return returnConfigOptions;
        }
    }
}
