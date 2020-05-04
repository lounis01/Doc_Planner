
using Doc_Planner.Identity;
using Doc_Planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doc_Planner.DAL
{
    public class DocPlannerContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public DocPlannerContext(DbContextOptions<DocPlannerContext> options) : base(options)
        {

        }
        public DbSet<Appointment> appointments { get; set; }
        

        
    }
}
