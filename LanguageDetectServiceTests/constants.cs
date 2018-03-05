using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace LanguageDetectServiceTests
{
    class Constants
    {
        public static readonly string APIKEY = WebConfigurationManager.AppSettings["apiKey"];
        public static readonly string SITEURL = WebConfigurationManager.AppSettings["siteURL"];
        public static readonly string RUNREPORTS = WebConfigurationManager.AppSettings["runReports"];
        public static readonly string REPORTLOCATION = WebConfigurationManager.AppSettings["reportLocation"];
        public static readonly string USERID = WebConfigurationManager.AppSettings["userID"];
        public static readonly string USERPASSWORD = WebConfigurationManager.AppSettings["userPassword"];


    }
}
