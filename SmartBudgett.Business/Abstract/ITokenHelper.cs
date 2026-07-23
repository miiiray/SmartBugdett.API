using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Abstract
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}
