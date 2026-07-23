using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBudgett.DTO.Incomes
{
    public class IncomeUpdateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
