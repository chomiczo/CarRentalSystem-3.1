using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CarRentalSystem.Models.CarModel>? CarModel { get; set; }
        public DbSet<CarRentalSystem.Models.CustomerModel>? CustomerModel { get; set; }
        public DbSet<CarRentalSystem.Models.RentalModel>? RentalModel { get; set; }
        public DbSet<CarRentalSystem.Models.Payment>? Payment { get; set; }
    }
}