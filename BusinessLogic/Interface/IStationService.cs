using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IStationService
    {
        Task<ResultAPI<EstacionDTO>> InsertStation(EstacionDTO estacion);
        Task<ResultAPI<IEnumerable<EstacionDTO>>> GetStation();
    }
}
