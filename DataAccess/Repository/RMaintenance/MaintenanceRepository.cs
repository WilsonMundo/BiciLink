using Domain.Interface;
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RMaintenance
{
    public class MaintenanceRepository : IMaintenanceRepository
    {

        private readonly SystemDBContext _dbContext;

        public MaintenanceRepository(SystemDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task<Mantenimiento> AddAsync(Mantenimiento mantenimiento)
        {
            _dbContext.Add(mantenimiento);
            await _dbContext.SaveChangesAsync();
            return mantenimiento;
        }

        public async Task<Mantenimiento?> GetMantenimiento(long mantenimientoId)
        {
            return await _dbContext.Mantenimientos
                .Include(x => x.Bicicleta)
                .Include(x => x.EstadoMantenimiento)
                .FirstOrDefaultAsync(x => x.MantenimientoId == mantenimientoId);
        }

        public async Task<List<Mantenimiento>> GetMantenimientos()
        {
            return await _dbContext.Mantenimientos
                .Include(x => x.Bicicleta)
                .Include(x => x.EstadoMantenimiento)
                .ToListAsync();
        }

        public async Task<bool> AnyMantenimiento(long mantenimientoId)
        {
            return await _dbContext.Mantenimientos.AnyAsync(x => x.MantenimientoId == mantenimientoId);
        }

        public async Task<Mantenimiento?> UpdateAsync(Mantenimiento mantenimiento)
        {
            var existing = await _dbContext.Mantenimientos
                .FirstOrDefaultAsync(x => x.MantenimientoId == mantenimiento.MantenimientoId);

            if (existing == null)
                return null;

            _dbContext.Entry(existing).CurrentValues.SetValues(mantenimiento);
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long mantenimientoId)
        {
            var mantenimiento = await _dbContext.Mantenimientos
                .FirstOrDefaultAsync(x => x.MantenimientoId == mantenimientoId);

            if (mantenimiento == null)
                return false;

            _dbContext.Mantenimientos.Remove(mantenimiento);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

