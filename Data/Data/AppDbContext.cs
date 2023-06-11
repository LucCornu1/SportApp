using Microsoft.EntityFrameworkCore;
using SportApplication.Data.Models;

// **
// Generate migrations : dotnet ef migrations add initial -o ./SportApplication/Data/Migrations
// Update migrations : dotnet ef migrations update

namespace SportApplication.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Participation> Participations { get; set; }
		public DbSet<Constraint> Constraints { get; set; }
		public DbSet<Sport> Sports { get; set; }
		

        public AppDbContext(DbContextOptions opt) : base(opt)
        {
			try
			{
				Database.EnsureCreated();
				if (Database.GetPendingMigrations().Count() > 0)
				{
					Database.Migrate();
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}
			
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(u => u.Roles)
				.WithMany(r => r.Users)
				.UsingEntity<UserRole>();

			modelBuilder.Entity<Role>()
				.HasData(
					new Role { Id = 1, Name = "admin" },
					new Role { Id = 2, Name = "user" }
				);

			modelBuilder.Entity<User>()
				.HasData(
					new User
					{
						Id = 1,
						Email = "admin@sportapp.com",
						Firstname = "Super",
						Lastname = "Admin",
						HashedPassword = "F2D81A260DEA8A100DD517984E53C56A7523D96942A834B9CDC249BD4E8C7AA9"
					}
				);

			modelBuilder.Entity<UserRole>()
				.HasData(
					new UserRole { Id = 1, UserId = 1, RoleId = 1 }
				);

			modelBuilder.Entity<Sport>()
				.HasData(
					new Sport("Ping Pong") { Id = 1 },
					new Sport("Tennis") { Id = 2 }
				);
		}
	}
}
