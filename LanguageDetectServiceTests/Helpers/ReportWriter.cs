using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using LanguageDetectServiceTests.DTO;

namespace LanguageDetectServiceTests.Helpers
{
    public static class ReportWriter
    {

        public static bool writeReport(Usage usage, Dictionary<string, string> failedTests, int passedTests, string testClass)
        {

            //Create our File path
            int reportEpoch = Utility.getEpoch();

            var path = Path.Combine("C:\\API_TEST_LOGS",
                DateTime.Now.Date.ToString("yyyy_MM_dd"),
                string.Format("Language API Test Report_{0}.txt", reportEpoch));

            var dir = Path.GetDirectoryName(path);

            //Check if the directory in our path is created. If not, create it.
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string fileLoc = string.Format(path);

            //Check if our file is already created. If not, create it.
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
                    sw.Write("Test Class: {0}\r\n\r\n", testClass);
                    sw.Write("Passed Tests: {0}\r\n\r\n", passedTests);

                    if (failedTests.Count == 0)
                    {
                        sw.Write("There were no failed test cases during this run!\r\n\r\n\r\n");
                    }
                    else
                    {
                        sw.Write("Failed Test Cases - {0} total:\r\n\r\n", failedTests.Count);

                        foreach (KeyValuePair<string, string> entry in failedTests)
                        {
                            sw.Write("Test Name: {0}\r\n", entry.Key);
                            sw.Write("Error Message: {0}\r\n\r\n", entry.Value);

                        }
                    }

                    sw.Write("API Usage Report for {0}.\r\n\n", usage.date);
                    sw.Write("----------------------------------\r\n");
                    sw.Write("Plan Type: {0}, Plan Status: {1}\r\n\n", usage.plan, usage.status);
                    sw.Write("We have used {0} of our {1} daily requests.\r\n\n", usage.requests, usage.dailyRequestsLimit);
                    sw.Write("{0} bytes have been used of our available {1} daily bytes.\r\n\n", usage.bytes, usage.dailyBytesLimit);
                }
            }

            return true;

        }


     
    }
}
