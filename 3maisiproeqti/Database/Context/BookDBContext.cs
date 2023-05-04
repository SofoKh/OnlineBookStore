using _3maisiproeqti.Database.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace _3maisiproeqti.Database.Context
{
    public class BookDBContext : DbContext
    {
        public BookDBContext()
        {
        }

        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        public  DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Server = (local); Database = OnlineBookStore; Trusted_Connection = True; ");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
