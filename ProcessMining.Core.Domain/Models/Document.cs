using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Models
{
    public class Document : DomainObject
    {
        public string Name { get; set; } = string.Empty;
        public int TransactionNummber { get; set; }
        public DocumentType DocumentType { get; set; }
        public override string ToString()
        {
            return string.Format("Transaction Name: {0}, TransactionNummber: {1}, DocumentType: {2}", Name, TransactionNummber.ToString(), DocumentType.ToString());
        }
    }
}
