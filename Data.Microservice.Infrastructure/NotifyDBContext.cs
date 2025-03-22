using Customer.Notify.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Notify.Microservice.Infrastructure
{
    public class NotifyDBContext : DbContext
    {
        public NotifyDBContext(DbContextOptions<NotifyDBContext> options)
            :base(options)
        {
            
        }


        public DbSet<Notification> NotifyDBC { get; set; }
    }
}
