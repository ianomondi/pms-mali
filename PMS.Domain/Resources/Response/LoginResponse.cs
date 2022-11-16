using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Response
{
    public class LoginResponse : BaseResponse<TokenGenerated>
    {
        public LoginResponse(TokenGenerated resource) : base(resource)
        {
        }

        public LoginResponse(string message) : base(message)
        {
        }

        public LoginResponse(TokenGenerated resource, string message) : base(resource, message)
        {
        }

        //public TokenGenerated data { get; set; }
    }

    public class TokenGenerated
    {
        public string token { get; set; }
        //public DateTime issuedAt { get; set; }
        public DateTime expiresAt { get; set; }
    }
}
