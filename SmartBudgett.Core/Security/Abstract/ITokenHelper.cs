using SmartBudgett.Entities;

namespace SmartBudgett.Core.Security.Abstract
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}
