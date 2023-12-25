using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.ViewModels
{
    public class RefreshReuqest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
