using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DataAcces
{
    public interface IUtilsRepository
    {
        long getSequence(string tableName);
    }
}
