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

    public class LangIDResult
    {
        public LangIDResultData data { get; set; }
    }

    public class LangIDResultData
    {
        public List<Detection> detections { get; set; }
    }

}
