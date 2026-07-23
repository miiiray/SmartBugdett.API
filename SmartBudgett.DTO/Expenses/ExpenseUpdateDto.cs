using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.DTO.Expenses
{
    public class ExpenseUpdateDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
