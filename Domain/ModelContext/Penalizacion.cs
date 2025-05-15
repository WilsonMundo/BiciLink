using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelContext
{

    public partial class Penalizacion
    {
        public long PenalizacionId { get; set; }

        public long UsuarioId { get; set; }

        public long ReservaId { get; set; }

        public long FechaInicio { get; set; }
    }
}
