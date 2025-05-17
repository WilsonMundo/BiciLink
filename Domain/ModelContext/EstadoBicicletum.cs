using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{
    public partial class EstadoBicicletum
    {
        public short EstadoBicicletaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Bicicletum> Bicicleta { get; set; } = new List<Bicicletum>();
    }
}
