using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class TipoSoporte
    {
        public short TipoSoporteId { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Soporte> Soportes { get; set; } = new List<Soporte>();
    }

}
