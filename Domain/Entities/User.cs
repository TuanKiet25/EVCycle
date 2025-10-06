using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Username { get; set; } 
        public string? Email { get; set; }
        public string? PasswordHash { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public string? AvatarUrl { get; set; }
        public Role Role { get; set; }
        public ICollection<Listing>? Listings { get; set; }
    }
}
