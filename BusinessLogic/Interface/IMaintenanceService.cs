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
        Task<ResultAPI<MantenimientoDTO>> PostMaintenance(MantenimientoDTO mantenimientoDTO, long idUsuario);
        Task<ResultAPI<List<MantenimientoDTO>>> GetMaintenanceList();
        Task<ResultAPI<MantenimientoDTO>> GetMaintenanceById(long mantenimientoId);
        Task<ResultAPI<MantenimientoDTO>> PutMaintenance(MantenimientoDTO mantenimientoDTO);
        Task<ResultAPI<bool>> DeleteMaintenance(long mantenimientoId);
    }
}
