using SmartBudgett.Entities;
using Volo.Abp;

namespace SmartBudgett.Business.Rules
{
    public class ExpenceBusinessRules
    {
        // Harcama tutarı geçerli mi?
        public void CheckIfAmountIsValid(decimal amount)
        {
            if (amount <= 0)
            {
                throw new BusinessException("Gider tutarı 0'dan büyük olmalıdır.");
            }
        }

        // Harcama açıklaması boş mu?
        public void CheckIfDescriptionIsValid(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new BusinessException("Gider açıklaması boş bırakılamaz.");
            }
        }

        // Harcama kaydı var mı?
        public void CheckIfExpenseExists(Expense? expense)
        {
            if (expense == null)
            {
                throw new BusinessException("İlgili gider kaydı bulunamadı.");
            }
        }
    }
}