using Domain.ModelsRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IUsuarioRepository
    {
        void add(Usuario usuario);
        Task AddAsync(Usuario usuario);
        Task Edit(Usuario usuario);
        Task<bool> ExistsUsuario(string email);
        Task<Usuario?> GetUsuarioAsync(string email);
    }
}
