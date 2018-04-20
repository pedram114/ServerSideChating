using System;
using System.ComponentModel.DataAnnotations;
using Common.UsingModels;

namespace Common.Model
{
    public class MessageReceipter
    {
        public MessageReceipter()
        {
            Id = Guid.NewGuid();
        }
        [Key] 
        public Guid Id { get; set; }
        public virtual ApplicationUser ReceipterUser { get; set; }
        public virtual UserMessage Messagecontent { get; set; }
        public bool IsRead { get; set; }            
    }
}