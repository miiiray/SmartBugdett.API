using FluentValidation;
using SmartBudgett.DTO.Auth;

namespace SmartBudgett.DTO.ValidationRules
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Ad alanı boş geçilemez.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("E-posta adresi boş geçilemez.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}