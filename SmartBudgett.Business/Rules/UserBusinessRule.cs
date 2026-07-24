using SmartBudgett.Entities;
using Volo.Abp;

namespace SmartBudgett.Business.Rules
{
    public class UserBusinessRule
    {
        // Kullanıcı adı/soyadı boş mu?
        public void CheckIfUserInfoIsValid(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new BusinessException("Kullanıcı ad ve soyad bilgisi boş bırakılamaz.");
            }
        }

        // Kullanıcı var mı?
        public void CheckIfUserExists(User? user)
        {
            if (user == null)
            {
                throw new BusinessException("Kullanıcı bulunamadı.");
            }
        }
    }
}