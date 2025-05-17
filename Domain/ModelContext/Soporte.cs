using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class Soporte
    {
        public long SoporteId { get; set; }

        public long UsuarioId { get; set; }

        public long? ReservaId { get; set; }

        public short TipoSoporteId { get; set; }

        public DateTime BtFecha { get; set; }

        public string Descripcion { get; set; } = null!;

        public string? Observacion { get; set; }

        public DateTime? FechaCierre { get; set; }

        public virtual Reserva? Reserva { get; set; }

        public virtual TipoSoporte TipoSoporte { get; set; } = null!;
    }

}
