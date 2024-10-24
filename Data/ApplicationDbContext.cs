using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Street = "123 Main St", City = "Charlotte", State = "NC" },
                new Address { Id = 2, Street = "456 Elm St", City = "Charlotte", State = "NC" },
                new Address { Id = 3, Street = "789 Maple Ave", City = "Michigan", State = "NC" }
            );

            modelBuilder.Entity<Club>().HasData(
                new Club { Id = 1, Title = "Running Club 1", Description = "This is the description of the first club", Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360", ClubCategory = ClubCategory.City, AddressId = 1 },
                new Club { Id = 2, Title = "Running Club 2", Description = "This is the description of the second club", Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360", ClubCategory = ClubCategory.Endurance, AddressId = 1 },
                new Club { Id = 3, Title = "Running Club 3", Description = "This is the description of the third club", Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360", ClubCategory = ClubCategory.Trail, AddressId = 3 }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race { Id = 1, Title = "Running Race 1", Description = "This is the description of the first race", Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360", RaceCategory = RaceCategory.Marathon, AddressId = 1 },
                new Race { Id = 2, Title = "Running Race 2", Description = "This is the description of the second race", Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360", RaceCategory = RaceCategory.Ultra, AddressId = 2 }
            );
        }
    }
}