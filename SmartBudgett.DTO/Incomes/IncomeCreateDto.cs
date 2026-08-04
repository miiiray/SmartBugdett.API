namespace SmartBudgett.DTO.Incomes
{
    public class IncomeCreateDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
