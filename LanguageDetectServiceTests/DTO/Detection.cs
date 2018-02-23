using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetectServiceTests.DTO
{

    public class Detection
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public float confidence { get; set; }
    }

    public class ResultData
    {
        public List<Detection> detections { get; set; }
    }

    public class Result
    {
        public ResultData data { get; set; }
    }

}
