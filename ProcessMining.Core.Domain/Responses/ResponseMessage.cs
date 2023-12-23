using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Responses
{
    public class ResponseMessage
    {
        public IEnumerable<string> Messages { get; set; }
        public ResponseMessage(string Message) : this(new List<string>() { Message }) { }
        public ResponseMessage(IEnumerable<string> messages)
        {
            Messages = messages;
        }
    }
}
