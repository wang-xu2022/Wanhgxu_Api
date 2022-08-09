using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangxuapi.Core.Model.Model;

namespace wangxuapi.Core.Model
{
    public  class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }
    }
}
