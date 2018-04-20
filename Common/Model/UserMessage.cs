using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Common.Model
{
    [Table("UserMessage")]
    public class UserMessage
    {
        [Key]
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime Receivedate { get; set; }
        public DateTime ReadDate { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
        public virtual List<MessageReceipter> MessageReceipters { set; get; }
        
    }
}