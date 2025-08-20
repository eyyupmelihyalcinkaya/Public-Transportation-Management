using internshipproject1.Domain.Enums;

namespace internshipproject1.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
      //  public UserRole Role { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public ICollection<RouteToCreate> CreatedRoutes { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
        public Customer Customer { get; set; }

    }
}
