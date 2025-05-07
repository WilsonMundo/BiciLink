using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsRegister
{
    public class Rol
    {
        public short RolId { get; set; }

        public string Nombre { get; set; } = null!;

        public bool FlagEmpleado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
