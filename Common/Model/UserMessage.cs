using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class UserMessage
    {
        [Key]
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime Receivedate { get; set; }
        public DateTime ReadDate { get; set; }
        public string FromUserId { get; set; }
        public virtual ApplicationUser InboxOfUser { get; set; }
        
    }
}