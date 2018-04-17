using System;

namespace Common.Model
{
    public class Client
    {
        public Guid Id { get; set; }
        public int CId { get; set; }
        public bool IsConnected { get; set; }
    }
}