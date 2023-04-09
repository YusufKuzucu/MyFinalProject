using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //SigningCredentialHelper=sen burada json web token sistemini yöneteceksin
    //senin güvenlik anahtarın budur=securityKey
    //şifreleme algoritman budur= SecurityAlgorithms.HmacSha512Signature
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
