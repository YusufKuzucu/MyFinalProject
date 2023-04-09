using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //kullacı adı ve şifre girdi ve apıye gitti 
    //eğer doğruysa CreateToken çalışcak
    //User ilgili kullanıcı için veritabanına gidicek veritabanında bu kullanıcının claimlerini buluşturcak
    //orada bir json web token üretcek onları geri vericek
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
      
    }
}
