using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_Management.Models;

namespace Task_Management.Data
{
    public class Task_ManagementContext : DbContext
    {
        public Task_ManagementContext (DbContextOptions<Task_ManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
