using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IReservaRepository
    {
        Task<Reserva> AddAsync(Reserva reserva);
        Task<Reserva?> GetReserva(long reservaId);
        Task<List<Reserva>> GetReservas();
        Task<bool> AnyReserva(long reservaId);
        Task<Reserva?> UpdateAsync(Reserva reserva);
        Task<bool> DeleteAsync(long reservaId);
    }
}
