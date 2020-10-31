using BloemenwinkelAPI.Model.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace BloemenwinkelAPI.Database
{
    //This is our gateway to the database
    public class BloemenwinkelDatabaseContext : DbContext
    {
        public BloemenwinkelDatabaseContext(DbContextOptions<BloemenwinkelDatabaseContext> ctx) : base(ctx)
        {

        }

        //A DbSet can be used to add/query items. It maps to a table.
        public DbSet<Bouqet> Bouqet { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
