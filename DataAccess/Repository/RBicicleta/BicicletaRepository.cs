using Domain.Interface.DataAcces;
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.RBicicleta
{
    public class BicicletaRepository: IBicicletaRepository
    {
        private readonly SystemDBContext _dbContext;
        public BicicletaRepository(SystemDBContext context)
        {
            this._dbContext = context;
        }
        public async Task<Bicicletum> Addasync(Bicicletum bicyle)
        {
            _dbContext.Add(bicyle);
            await _dbContext.SaveChangesAsync();
            return bicyle;
        }
        public async Task<Bicicletum?> GetBicycle(long bicycleId)
        {
            var result = await _dbContext.Bicicleta.FirstOrDefaultAsync(x => x.BicicletaId == bicycleId);
            return result;
        }
        public async Task<List<Bicicletum>> GetBicycles()
        {
            var result = await _dbContext.Bicicleta.Include(x=> x.EstadoBicicleta).ToListAsync();
            return result;
        }
        public async Task<bool> AnyBicycle(long bicycleId)
        {
            return await _dbContext.Bicicleta.AnyAsync(x => x.BicicletaId == bicycleId);
        }
    }
}
