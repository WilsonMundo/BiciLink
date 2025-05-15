using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class EstacionDTO
    {
        public long estacionId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public decimal? Longitud { get; set; }
        [Required]
        public decimal? Latitud { get; set; }
        
        public bool FlagActivo { get; set; }
        
    }
}
