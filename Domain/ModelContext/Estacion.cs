using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class Estacion
    {
        public long EstacionId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public decimal? Longitud { get; set; }

        public decimal? Latitud { get; set; }
        public bool FlagActivo { get; set; }

        public virtual ICollection<Reserva> ReservaEstacionDestinos { get; set; } = new List<Reserva>();

        public virtual ICollection<Reserva> ReservaEstacionOrigens { get; set; } = new List<Reserva>();
    }
}
