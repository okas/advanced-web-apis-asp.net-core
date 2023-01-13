using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext (DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.Models.Product> Product { get; set; } = default!;
        
        public DbSet<WebAPI.Models.Category> Categories { get; set; } = default!;

    }
}
