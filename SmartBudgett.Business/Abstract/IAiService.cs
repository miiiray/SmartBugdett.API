namespace SmartBudgett.Business.Abstract.Services
{
    public interface IAiService
    {
        Task<string> AnalyzeBudgetAsync(int userId);
    }
}