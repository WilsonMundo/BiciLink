using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IEstadoReservaRepository
    {
        Task<EstadoReserva> AddAsync(EstadoReserva estadoReserva);
        Task<List<EstadoReserva>> GetEstadoReservasAsync();
        Task<EstadoReserva?> GetEstadoReservaByIdAsync(short idEstado);
        Task<bool> DeleteAsync(short id);
        Task<bool> SaveAsync();
    }
}
