namespace Common.UsingModels
{
    public class Message
    {
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Text { get; set; }
        public long? MessageId { get; set; }
    }
}