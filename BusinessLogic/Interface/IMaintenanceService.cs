using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMaintenanceService
    {
        Task<ResultAPI<GStateGeneralDTO>> PostState(GEstadoDTO estadoDTO);
        Task<ResultAPI<GStateGeneralDTO>> PutState(GStateGeneralDTO gStateGeneral);
        Task<ResultAPI<List<GStateGeneralDTO>>> GetState();
        Task<ResultAPI<object>> DeleteState(short id);

    }
}
