
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class SystemDBContext : DbContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {

        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
              
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
