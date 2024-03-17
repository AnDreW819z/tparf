using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tparf.dto.Auth
{
    public class LoginResponse : Status
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long CartId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }

    }
}
