using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface  IMaintenanceRepository
    {
        Task<EstadoMantenimiento> Addasync(EstadoMantenimiento estado);
        Task<List<EstadoMantenimiento>> GetEstadoMantenimientoAsync();
        Task<EstadoMantenimiento?> GetEstadoMantenimiento(short idEstado);
        Task<bool> SaveAsync();
        Task<bool> DeleteAsync(short id);
        Task<short> MaxIdAsync();
    }
}
