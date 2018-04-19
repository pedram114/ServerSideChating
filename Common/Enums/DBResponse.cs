using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Common.Enums
{
    public class DBResponse
    {
        public DBResponse()
        {
            IsSucced = true;
            Messages=new List<string>();
        }
        public bool IsSucced { get; set; }
        public List<string> Messages { get; set; }
        
        
    }
}