using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class Reserva
    {
        public long ReservaId { get; set; }

        public long UsuarioId { get; set; }

        public long BicicletaId { get; set; }

        public long EstacionOrigenId { get; set; }

        public long EstacionDestinoId { get; set; }

        public string? CodigoDesbloqueo { get; set; }

        public DateTime FechaReserva { get; set; }

        public short EstadoReservaId { get; set; }

        public DateTime? BtFecha { get; set; }

        public virtual Bicicletum Bicicleta { get; set; } = null!;

        public virtual Estacion EstacionDestino { get; set; } = null!;

        public virtual Estacion EstacionOrigen { get; set; } = null!;

        public virtual EstadoReserva EstadoReserva { get; set; } = null!;

        public virtual ICollection<Soporte> Soportes { get; set; } = new List<Soporte>();
    }

}
