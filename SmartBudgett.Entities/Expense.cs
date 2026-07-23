using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.Entities
{
    public class Expense : BaseEntity
    {
        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
