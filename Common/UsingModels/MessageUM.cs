using System;

namespace Common.UsingModels
{
    public class MessageUM
    {
        public MessageUM()
        {
            Sendtry = 0;
        }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Text { get; set; }
        public DateTime SendDate { set; get; }
        public DateTime ReceiveDate { set; get; }
        public DateTime ReadDate { set; get; }
        public int Sendtry { get; set; }
    }
}