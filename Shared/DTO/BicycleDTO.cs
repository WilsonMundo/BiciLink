using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class BicycleDTO
    {
        [Required]
        public short EstadoBicicletaId { get; set; }
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string codigo { get; set; } = null!;

        public string? Caracteristica { get; set; }

        public string? Imagen { get; set; }

        public string? Imagen1 { get; set; }     
        public string? EstadoString {  get; set; }
        public long? bicycleId { get; set; }

    }
}
