using Microsoft.EntityFrameworkCore;

namespace BluePaw.Administration.Data
{
    public class BluePawDbContext : DbContext
    {
        public DbSet<AdministrationRequest> AdministrationRequests { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public BluePawDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
