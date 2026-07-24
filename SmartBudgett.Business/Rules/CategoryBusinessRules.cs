using SmartBudgett.Entities;
using Volo.Abp;

namespace SmartBudgett.Business.Rules
{
    public class CategoryBusinessRules
    {
        // Kategori adı geçerli mi?
        public void CheckIfCategoryNameIsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessException("Kategori adı boş geçilemez.");
            }

            if (name.Length < 2)
            {
                throw new BusinessException("Kategori adı en az 2 karakter olmalıdır.");
            }
        }

        // Silinecek veya güncellenecek kategori var mı?
        public void CheckIfCategoryExists(Category? category)
        {
            if (category == null)
            {
                throw new BusinessException("Aranan kategori bulunamadı.");
            }
        }
    }
}