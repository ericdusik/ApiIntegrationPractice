using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetectServiceTests.DTO
{

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }

    }

    class ErrorResponse
    {
        public Error error { get; set; }
    }

}
