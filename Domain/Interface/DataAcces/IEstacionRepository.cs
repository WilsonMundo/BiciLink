using Domain.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IEstacionRepository
    {
        Task<Estacion> Addasync(Estacion estacion);
        Task<Estacion?> GetEstacion(long estacionId);
        Task<List<Estacion>> GetEstacions();
        Task<bool> AnyEstacion(long estacionId);

    }
}
