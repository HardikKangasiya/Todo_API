using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=CustomConnection")
        {
        }

        // Add DbSets for your other entities here
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
