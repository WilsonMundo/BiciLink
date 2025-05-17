using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{

    public partial class EstadoReserva
    {
        public short EstadoReservaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
