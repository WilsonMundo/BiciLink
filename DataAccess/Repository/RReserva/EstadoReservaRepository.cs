using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RReserva
{
    public class EstadoReservaRepository: IEstadoReservaRepository
    {
        private readonly SystemDBContext _dbContext;

        public EstadoReservaRepository(SystemDBContext context)
        {
            this._dbContext = context;
        }

        public async Task<EstadoReserva> AddAsync(EstadoReserva estadoReserva)
        {
            _dbContext.Add(estadoReserva);
            await _dbContext.SaveChangesAsync();
            return estadoReserva;
        }

        public async Task<List<EstadoReserva>> GetEstadoReservasAsync()
        {
            return await _dbContext.EstadoReservas.ToListAsync();
        }

        public async Task<EstadoReserva?> GetEstadoReservaByIdAsync(short idEstado)
        {
            return await _dbContext.EstadoReservas.FirstOrDefaultAsync(x => x.EstadoReservaId == idEstado);
        }

        public async Task<bool> DeleteAsync(short id)
        {
            var entity = await _dbContext.EstadoReservas
                .FirstOrDefaultAsync(x => x.EstadoReservaId == id);

            if (entity == null)
                return false;

            _dbContext.EstadoReservas.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
