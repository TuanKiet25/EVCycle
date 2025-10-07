using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "Jwt";
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public string Key { get; init; } = null!;
    }
}
