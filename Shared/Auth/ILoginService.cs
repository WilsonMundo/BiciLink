using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth
{
    public interface ILoginService
    {
        Task Login();
        Task LogOut();
    }
}
