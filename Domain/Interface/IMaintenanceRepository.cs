using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IMaintenanceRepository
    {
        Task<Mantenimiento> AddAsync(Mantenimiento mantenimiento);
        Task<Mantenimiento?> GetMantenimiento(long mantenimientoId);
        Task<List<Mantenimiento>> GetMantenimientos();
        Task<bool> AnyMantenimiento(long mantenimientoId);
        Task<Mantenimiento?> UpdateAsync(Mantenimiento mantenimiento);
        Task<bool> DeleteAsync(long mantenimientoId);
    }
}
