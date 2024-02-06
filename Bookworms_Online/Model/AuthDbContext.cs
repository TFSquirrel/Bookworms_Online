using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bookworms_Online.Model;

namespace Bookworms_Online.Model
{
    public class AuthDbContext : IdentityDbContext<Bookworms_Online.Model.Member>
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        private readonly IConfiguration _configuration;
        public AuthDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AuthConnectionString"); optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
