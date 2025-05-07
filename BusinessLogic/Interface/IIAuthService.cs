using BusinessLogic.DTO;
using Shared.Auth.DTO;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IIAuthService
    {
        Task<ResultAPI<object>> RegisterUser(UserRegister user);
        Task<ResultAPI<UserToken?>> ValidateUser(UserInfo userDTO);
    }
}
