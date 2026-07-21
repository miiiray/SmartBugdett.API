using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.Entities
{
    public class Category : BaseEntitiy
    {
        public required string Name { get; set; }
        public int UserId { get; set; }
    }
}
