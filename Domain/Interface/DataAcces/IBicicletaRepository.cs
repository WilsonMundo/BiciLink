using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IBicicletaRepository
    {
        Task<Bicicletum> Addasync(Bicicletum bicyle);
        Task<Bicicletum?> GetBicycle(long bicycleId);
        Task<List<Bicicletum>> GetBicycles();
        Task<bool> AnyBicycle(long bicycleId);
    }
}
