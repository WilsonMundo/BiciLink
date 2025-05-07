using Domain.Interface.DataAcces;
using Domain.ModelsRegister;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository.Seguridad
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RegisterDBContext _dbContext;
        public UsuarioRepository(RegisterDBContext context)
        {
            this._dbContext = context;
        }
        public void add(Usuario usuario)
        {
            _dbContext.Add(usuario);
        }
        public async Task AddAsync(Usuario usuario)
        {
            _dbContext.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Edit(Usuario usuario)
        {
            _dbContext.Entry(usuario).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsUsuario(string email)
        {
            return await _dbContext.Usuarios.AnyAsync(x => x.Email == email);
        }
        public async Task<Usuario?> GetUsuarioAsync(string email)
        {
            return await _dbContext.Usuarios.Where(x => x.Email == email).Include(x => x.Rol).FirstOrDefaultAsync();
        }
    }
}
