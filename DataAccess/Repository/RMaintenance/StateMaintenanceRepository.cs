using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RMaintenance
{
    public class StateMaintenanceRepository : IStateMaintenanceRepository
    {
        private readonly SystemDBContext _dbContext;

        public StateMaintenanceRepository(SystemDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task<EstadoMantenimiento> Addasync(EstadoMantenimiento estado)
        {
            _dbContext.Add(estado);
            await _dbContext.SaveChangesAsync();
            return estado;
        }
        public async Task<List<EstadoMantenimiento>> GetEstadoMantenimientoAsync()
        {
            return await _dbContext.EstadoMantenimientos.ToListAsync();
        }
        public async Task<EstadoMantenimiento?> GetEstadoMantenimiento(short idEstado)
        {
            return await _dbContext.EstadoMantenimientos.FirstOrDefaultAsync(x => x.EstadoMantenimientoId == idEstado);
        }
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(short id)
        {
            var entity = await _dbContext.EstadoMantenimientos
                .FirstOrDefaultAsync(x => x.EstadoMantenimientoId == id);

            if (entity == null)
                return false;

            _dbContext.EstadoMantenimientos.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<short> MaxIdAsync()
        {
            short idConsecutivo  = await _dbContext.EstadoMantenimientos.MaxAsync(x => (short?)x.EstadoMantenimientoId) ??0;            
            idConsecutivo = (short)(idConsecutivo + 1); ;
            return idConsecutivo;
        }
    }
}
