using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ReservaDTO
    {
        public long ReservaId { get; set; }

        [Required]
        public long BicicletaId { get; set; }
        [Required]
        public long EstacionOrigenId { get; set; }
        [Required]
        public long EstacionDestinoId { get; set; }

        public string? CodigoDesbloqueo { get; set; }
        [Required]
        public DateTime FechaReserva { get; set; }

        public short EstadoReservaId { get; set; }

    }
}
