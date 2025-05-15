using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IBicycleService
    {
        Task<ResultAPI<BicycleDTO>> PostBicycle(BicycleDTO bicicletum);
        Task<ResultAPI<List<BicycleDTO>>> GetBicycles();
    }
}
