using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class TokenInfo
    {
        public string email { get; set; } = null!;
        public long idUser { get; set; }        
        public string name { get; set; } = null!;                        
        public string? rolAdmin { get; set; }
    }
}
