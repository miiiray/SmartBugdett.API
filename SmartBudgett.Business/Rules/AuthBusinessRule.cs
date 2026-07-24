using SmartBudgett.Entities;
using Volo.Abp;

namespace SmartBudgett.Business.Rules
{
    public class AuthBusinessRule
    {
        // E-posta adresi sistemde zaten var mı?
        public void CheckIfEmailAlreadyExists(User? user)
        {
            if (user != null)
            {
                throw new BusinessException("Bu e-posta adresiyle zaten kayıtlı bir kullanıcı var.");
            }
        }

        // Giriş yaparken kullanıcı bulundu mu ve şifre doğru mu?
        public void CheckIfUserOrPasswordIsInvalid(User? user, bool isPasswordValid)
        {
            if (user == null || !isPasswordValid)
            {
                throw new BusinessException("E-posta veya şifre hatalı.");
            }
        }
    }
}