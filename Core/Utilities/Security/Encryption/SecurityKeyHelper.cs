using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //appSetting.jsonda SecurrityKey var 
    //SecurityKeyHelper=appSettings.json daki SecurityKey i byte array haline getiriyor
    //ve bytenı alıp onu simetrik bir anahtar hale getiriyor
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
