using Microsoft.EntityFrameworkCore;

namespace musicStoreMVC.Models
{
    public class MusicStoreEntities : DbContext
    {
        private string _connectionString = "Server=.; Database=MusicStoreEntitiesDataBase; Trusted_Connection=True;";
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> options) : base(options) { }

       public MusicStoreEntities() : base() { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Album>().HasData()
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //   dbContextOptionsBuilder.UseSqlServer(_connectionString);
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("MusicStoreEntitiesDataBase");
            dbContextOptionsBuilder.UseSqlServer(connectionString);
        }
    }
}

