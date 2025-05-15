using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RBicicleta
{
    public class EstadoBicicletaRepository : IEstadoBicicletaRepository
    {
        private readonly SystemDBContext _dbContext;
        public EstadoBicicletaRepository(SystemDBContext context)
        {
            this._dbContext = context;
        }
        public async Task<EstadoBicicletum> Addasync(EstadoBicicletum estadoBicicletum)
        {
            _dbContext.Add(estadoBicicletum);
            await _dbContext.SaveChangesAsync();
            return estadoBicicletum;
        }
        public async Task<List<EstadoBicicletum>> GetEstadoBicicletaAsync()
        {
            return await _dbContext.EstadoBicicleta.ToListAsync();
        }
        public async Task<EstadoBicicletum?> GetEstadoBicicletum(short idEstado)
        {
            return await _dbContext.EstadoBicicleta.FirstOrDefaultAsync(x => x.EstadoBicicletaId == idEstado);
        }
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(short id)
        {
            var entity = await _dbContext.EstadoBicicleta
                .FirstOrDefaultAsync(x => x.EstadoBicicletaId == id);

            if (entity == null)
                return false;

            _dbContext.EstadoBicicleta.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
