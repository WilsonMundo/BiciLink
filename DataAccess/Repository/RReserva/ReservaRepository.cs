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
    public class ReservaRepository: IReservaRepository
    {
        private readonly SystemDBContext _dbContext;

        public ReservaRepository(SystemDBContext systemDBContext)
        {
            this._dbContext = systemDBContext;
        }
        public async Task<Reserva> AddAsync(Reserva reserva)
        {
            _dbContext.Add(reserva);
            await _dbContext.SaveChangesAsync();
            return reserva;
        }

        public async Task<Reserva?> GetReserva(long reservaId)
        {
            return await _dbContext.Reservas
                .Include(x => x.Bicicleta)
                .Include(x => x.EstacionOrigen)
                .Include(x => x.EstacionDestino)
                .Include(x => x.EstadoReserva)
                .FirstOrDefaultAsync(x => x.ReservaId == reservaId);
        }

        public async Task<List<Reserva>> GetReservas()
        {
            return await _dbContext.Reservas
                .Include(x => x.Bicicleta)
                .Include(x => x.EstacionOrigen)
                .Include(x => x.EstacionDestino)
                .Include(x => x.EstadoReserva)
                .ToListAsync();
        }

        public async Task<bool> AnyReserva(long reservaId)
        {
            return await _dbContext.Reservas.AnyAsync(x => x.ReservaId == reservaId);
        }

        public async Task<Reserva?> UpdateAsync(Reserva reserva)
        {
            var existing = await _dbContext.Reservas
                .FirstOrDefaultAsync(x => x.ReservaId == reserva.ReservaId);

            if (existing == null)
                return null;

            _dbContext.Entry(existing).CurrentValues.SetValues(reserva);
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long reservaId)
        {
            var reserva = await _dbContext.Reservas
                .FirstOrDefaultAsync(x => x.ReservaId == reservaId);

            if (reserva == null)
                return false;

            _dbContext.Reservas.Remove(reserva);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
