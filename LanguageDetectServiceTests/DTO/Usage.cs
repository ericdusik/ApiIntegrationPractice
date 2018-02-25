using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetectServiceTests.DTO
{

    public class Usage
    {
        public string date { get; set; }
        public int requests { get; set; }
        public int bytes { get; set; }
        public string plan { get; set; }
        public int dailyRequestsLimit { get; set; }
        public int dailyBytesLimit { get; set; }
        public string status { get; set; }
        
    }

}
