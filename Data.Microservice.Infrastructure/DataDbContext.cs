using Data.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.Infrastructure
{
    public class DataDbContext : DbContext
    {

        public DataDbContext(DbContextOptions<DataDbContext> options)
            :base(options)
        {
            
        }


        public DbSet<CData> DataDomain { get; set; }
    }
}
