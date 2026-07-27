namespace SmartBudgett.Core.Constants
{
    public static class Messages
    {
        // Yetkilendirme (Auth) Mesajları
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "E-posta veya şifre hatalı.";
        public static string SuccessfulLogin = "Sisteme giriş başarılı.";
        public static string UserAlreadyExists = "Bu e-posta adresiyle zaten kayıtlı bir kullanıcı var.";
        public static string AccessTokenCreated = "Erişim token'ı başarıyla oluşturuldu.";

        // Kategori, Harcama ve Gelir (CRUD) Mesajları
        public static string Added = "Kayıt başarıyla eklendi.";
        public static string Updated = "Kayıt başarıyla güncellendi.";
        public static string Deleted = "Kayıt başarıyla silindi.";
        public static string Listed = "Kayıtlar başarıyla listelendi.";
        public static string NotFound = "İlgili kayıt bulunamadı.";

        // İş Kuralı (Business Rule) Hata Mesajları
        public static string InvalidAmount = "Tutar 0'dan büyük olmalıdır.";
        public static string InvalidName = "Geçersiz veya boş isim girdiniz.";
    }
}