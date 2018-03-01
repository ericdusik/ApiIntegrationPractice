using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Web.Configuration;
using LanguageDetectServiceTests.DTO;

namespace LanguageDetectServiceTests.Helpers
{
    public static class ReportWriter
    {

        public static bool writeReport(Usage usage)
        {

            int reportEpoch = Utility.getEpoch();

            var path = Path.Combine(WebConfigurationManager.AppSettings["reportLocation"],
                DateTime.Now.Date.ToString("yyyy_MM_dd"),
                string.Format("Language API Test Report_{0}.txt", reportEpoch));

            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string fileLoc = string.Format(path);

            FileStream fs = null;
            if (!File.Exists(fileLoc))
            {
                using (fs = File.Create(fileLoc))
                {

                }
            }



            //Write test result + API usage report.
            if (File.Exists(fileLoc))
            {
                using (StreamWriter sw = new StreamWriter(fileLoc))
                {
                    sw.Write("detectlanguage.com API Test Report\r\n");
                    sw.Write("----------------------------------\r\n\r\n");
                    sw.Write("API Usage Report for {0}.\r\n\n", usage.date);
                    sw.Write("Plan Type: {0}, Plan Status: {1}\r\n\n", usage.plan, usage.status);
                    sw.Write("We have used {0} of our {1} daily requests.\r\n\n", usage.requests, usage.dailyRequestsLimit);
                    sw.Write("{0} bytes have been used of our available {1} daily bytes.\r\n\n", usage.bytes, usage.dailyBytesLimit);
                }
            }

            return true;

        }


     
    }
}
