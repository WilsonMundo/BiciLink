using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class Bicicletum
    {
        public long BicicletaId { get; set; }

        public short EstadoBicicletaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public string? Caracteristica { get; set; }

        public string? Imagen { get; set; }

        public string? Imagen1 { get; set; }
        public string? codigo { get; set; }

        public virtual EstadoBicicletum EstadoBicicleta { get; set; } = null!;

        public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
