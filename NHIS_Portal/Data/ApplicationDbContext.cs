using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NHIS_Portal.Models.AuthorizationCode;
using NHIS_Portal.Models.Entities;

namespace NHIS_Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<TreatmentType> TreatmentType { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EnroleeComplain> EnroleeComplain { get; set; }
        public DbSet<ProviderDetails> ProviderDetails { get; set; }
        public DbSet<AuthorizationCode> AuthorizationCodes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            SeedLocation(builder);
            SeedTreatmentType(builder);
            SeedDepartment(builder);
            SeedBranch(builder);
            SeedUserRole(builder);
        }
        //Seed into Db
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
        private void SeedTreatmentType(ModelBuilder builder)
        {
            builder.Entity<TreatmentType>().HasData
                (
                new TreatmentType() { TreatmentName = "Admission", Id = 1 },
                new TreatmentType() { TreatmentName = "Antenatal", Id = 2 },
                new TreatmentType() { TreatmentName = "Surgery", Id = 3 },
                new TreatmentType() { TreatmentName = "Others", Id = 4 }
                );
        }
        private void SeedLocation(ModelBuilder builder)
        {
            builder.Entity<Location>().HasData
                (
                new Location() { Name = "Abuja", Id = 1 },
                new Location() { Name = "Benin", Id = 2 },
                new Location() { Name = "Enugu", Id = 3 },
                new Location() { Name = "Gombe", Id = 4 },
                new Location() { Name = "Ibadan", Id = 5 },
                new Location() { Name = "Ife", Id = 6 },
                new Location() { Name = "Kaduna", Id = 7 },
                new Location() { Name = "Kano", Id = 8 },
                new Location() { Name = "Lagos", Id = 9 },
                new Location() { Name = "Maiduguri", Id = 10 },
                new Location() { Name = "Port Harcourt", Id = 11 }
                );
        }
        private void SeedDepartment(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData
                (
                new Department() { Id = 1, Name = "Call Centre" },
                new Department() { Id = 2, Name = "ICT" },
                new Department() { Id = 3, Name = "ICU" },
                new Department() { Id = 4, Name = "QUALITY Assurance" }
                );
        }
        private void SeedBranch(ModelBuilder builder)
        {
            builder.Entity<Branch>().HasData
                (
                new Branch() { Name = "Abuja", Id = 1 },
                new Branch() { Name = "Benin", Id = 2 },
                new Branch() { Name = "Enugu", Id = 3 },
                new Branch() { Name = "Gombe", Id = 4 },
                new Branch() { Name = "Ibadan", Id = 5 },
                new Branch() { Name = "Ife", Id = 6 },
                new Branch() { Name = "Kaduna", Id = 7 },
                new Branch() { Name = "Kano", Id = 8 },
                new Branch() { Name = "Lagos", Id = 9 },
                new Branch() { Name = "Maiduguri", Id = 10 },
                new Branch() { Name = "Port Harcourt", Id = 11 }
                );
        }
        private void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasData
                (
                new UserRole() { Id = 1, Name = "Call centre" },
                new UserRole() { Id = 2, Name = "Outstation" },
                new UserRole() { Id = 3, Name = "QA" },
                new UserRole() { Id = 4, Name = "Claims" },
                new UserRole() { Id = 5, Name = "Audit" },
                new UserRole() { Id = 6, Name = "Verification Team" },
                new UserRole() { Id = 7, Name = "Verification Team Lead" }
                );
                
        }
    }
}
