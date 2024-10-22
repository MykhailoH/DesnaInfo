using System.Reflection;
using DesnaInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DesnaInfo.DataAccess
{
    public sealed class DesnaInfoDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DesnaInfoDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = _configuration["DatabaseName"];
            optionsBuilder.UseSqlite(connectionString:$"FileName={dbName}", option =>
                {
                    option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>(entity => { entity.HasKey(k => k.Id); });
            modelBuilder.Entity<User>(entity => { entity.HasIndex(i => i.MessengerId).IsUnique(); });

            base.OnModelCreating(modelBuilder);
        }
    }
}
