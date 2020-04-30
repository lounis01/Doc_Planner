
using Doc_Planner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doc_Planner.DAL
{
    public class DocPlannerContext : DbContext
    {
        public DocPlannerContext(DbContextOptions<DocPlannerContext> options) : base(options)
        {

        }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<User> users { get; set; }

        
    }
}
