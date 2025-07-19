using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IReservaService
    {
        Task<ResultAPI<ReservaDTO>> PostReserva(ReservaDTO reservaDTO, long usuarioId);
        Task<ResultAPI<List<ReservaDTO>>> GetReservaList();
        Task<ResultAPI<ReservaDTO>> GetReservaById(long reservaId);
        Task<ResultAPI<ReservaDTO>> PutReserva(ReservaDTO reservaDTO);
        Task<ResultAPI<bool>> DeleteReserva(long reservaId);
    }
}
