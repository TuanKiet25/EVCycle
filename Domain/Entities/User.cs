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
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? Imgs { get; set; }
        public bool IsVerified { get; set; } = false; 
        public string? VerificationOtp { get; set; } 
        public DateTime? OtpExpiryTime { get; set; } 
        public Role Role { get; set; } 
        public ICollection<Listing>? Listings { get; set; }

    }
}
