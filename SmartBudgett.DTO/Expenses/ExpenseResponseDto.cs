using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.DTO
{
    public class ExpenseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
    }
}
