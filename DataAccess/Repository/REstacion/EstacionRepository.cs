using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Domain.ModelsRegister;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.REstacion
{
    public class EstacionRepository: IEstacionRepository
    {
        private readonly SystemDBContext _dbContext;
        public EstacionRepository(SystemDBContext context)
        {
            this._dbContext = context;
        }
        public async Task<Estacion> Addasync(Estacion estacion)
        {
            _dbContext.Add(estacion);
            await _dbContext.SaveChangesAsync();
            return estacion;
        }
        public async Task<Estacion?> GetEstacion(long estacionId)
        {
            var result = await _dbContext.Estacions.FirstOrDefaultAsync(x=> x.EstacionId  == estacionId);
            return result;
        }
        public async Task<List<Estacion>> GetEstacions()
        {
            var result = await _dbContext.Estacions.Where(x=> x.FlagActivo == true).ToListAsync();
            return result;
        }
        public async Task<bool> AnyEstacion(long estacionId)
        {
            return await _dbContext.Estacions.AnyAsync(x=> x.EstacionId == estacionId);
        }
    }
}
