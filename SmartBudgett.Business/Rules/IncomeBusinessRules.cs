using SmartBudgett.Entities;
using Volo.Abp;

namespace SmartBudgett.Business.Rules
{
    public class IncomeBusinessRules
    {
        // Gelir tutarı geçerli mi?
        public void CheckIfAmountIsValid(decimal amount)
        {
            if (amount <= 0)
            {
                throw new BusinessException("Gelir tutarı 0'dan büyük olmalıdır.");
            }
        }

        // Gelir açıklaması geçerli mi?
        public void CheckIfDescriptionIsValid(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new BusinessException("Gelir açıklaması boş bırakılamaz.");
            }
        }

        // Gelir kaydı var mı?
        public void CheckIfIncomeExists(Income? income)
        {
            if (income == null)
            {
                throw new BusinessException("İlgili gelir kaydı bulunamadı.");
            }
        }
    }
}