// **
// Here we do not put User or Role because the framework do it for us because of this line : modelBuilder.Entity<User>()
//				.HasMany(u => u.Roles)
//				.WithMany(r => r.Users)
//				.UsingEntity<UserRole>();

namespace SportApplication.Data.Models
{
	public class UserRole : Entity
	{
		public int UserId { get; set; }

		public int RoleId { get; set; }
	}
}
