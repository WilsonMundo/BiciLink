using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IEstadoBicicletaRepository
    {
        Task<EstadoBicicletum> Addasync(EstadoBicicletum estadoBicicletum);
        Task<List<EstadoBicicletum>> GetEstadoBicicletaAsync();
        Task<EstadoBicicletum?> GetEstadoBicicletum(short idEstado);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync(short id);
    }
}
