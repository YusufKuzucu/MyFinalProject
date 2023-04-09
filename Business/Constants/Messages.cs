using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi"; 
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "sistem bakımda";
        public static string ProductListed = "urunler listelendi";
        public static string ProductCountOfCategoryError = "1 kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Böyle bir isim bulunmakta başka isim deneyin";
        public static string CategoryLimitExceded = "category limiti aşıldı";
        public static string AuthorizationDenied = "yetkin yok";
        public static string UserRegistered = "kayıt oldu";
        public static string UserNotFound = "kullanıcı bulunamadı";
        public static string PasswordError = "parola hatası";
        public static string SuccessfulLogin = "başarılı giriş";
        public static string UserAlreadyExists = "kullanıcı mevcut var";
        public static string AccessTokenCreated = "token oluşturuldu";
    }
}
