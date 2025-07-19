using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class Mantenimiento
    {
        public long MantenimientoId { get; set; }

        public long TecnicoId { get; set; }

        public long BicicletaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public string? Observacion { get; set; }

        public DateTime BtFecha { get; set; }

        public DateTime? FechaFin { get; set; }

        public short EstadoMantenimientoId { get; set; }

        public virtual Bicicletum Bicicleta { get; set; } = null!;
        public virtual EstadoMantenimiento EstadoMantenimiento { get; set; } = null!;
    }

}
