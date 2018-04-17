using Common.Enums;

namespace Common.UsingModels
{
    public class Response
    {
        public ResponseStatus code { get; set; }
        public string text { get; set; }
        public string responsedata { set; get; }
    }
}