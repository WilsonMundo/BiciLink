using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class MantenimientoDTO
    {
        public long MantenimientoId { get; set; }
        [Required]
        public long BicicletaId { get; set; }
        [Required]
        public string Descripcion { get; set; } = null!;

        public string? Observacion { get; set; }
        
        public DateTime? FechaFin { get; set; }
        [Required]
        public short EstadoMantenimientoId { get; set; }
        public string? EstadoMantenimiento {  get; set; }
    }
}
