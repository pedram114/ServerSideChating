using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.UsingModels
{
    public class Response
    {
        public ResponseStatus code { get; set; }
        public string text { get; set; }    
        public string responsedata { set; get; }
    }
}
