using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloemenwinkelAPI.Database
{
    //This is our gateway to the database
    public class BloemenwinkelDatabaseContext : DbContext
    {
        public BloemenwinkelDatabaseContext(DbContextOptions<BloemenwinkelDatabaseContext> ctx) : base(ctx)
        {

        }

        //A DbSet can be used to add/query items. It maps to a table.
        public DbSet<Boeket> Boeket { get; set; }
        public DbSet<Winkel> Winkel { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
}