using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class EstadoMantenimiento
    {
        public short EstadoMantenimientoId { get; set; }

        public string Descripcion { get; set; } = null!;
        public virtual ICollection<Mantenimiento> Mantenimiento { get; set; } = new List<Mantenimiento>();
    }
}
