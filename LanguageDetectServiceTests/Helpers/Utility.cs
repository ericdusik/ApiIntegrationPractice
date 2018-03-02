using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace LanguageDetectServiceTests.Helpers
{
    class Utility
    {
        public static int getEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }

        public static bool logError(TestContext context, Dictionary<string, string> failedTestCases)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                failedTestCases.Add(TestContext.CurrentContext.Test.FullName.ToString(),
                                    TestContext.CurrentContext.Result.Message.ToString());
            }

            return true;
        }

    }
}
