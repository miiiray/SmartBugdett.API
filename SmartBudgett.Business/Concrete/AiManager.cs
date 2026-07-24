using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;
using SmartBudgett.DataAccess.Abstract;

namespace SmartBudgett.Business.Concrete.Managers
{
    public class AiManager : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
     
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;

        public AiManager(HttpClient httpClient,
                IConfiguration configuration,
                IIncomeService incomeService,
                IExpenseService expenseService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _incomeService = incomeService;
            _expenseService = expenseService;
        }
        public async Task<string> AnalyzeBudgetAsync(int userId)
        {
            var incomes = await _incomeService.GetAllAsync();
            var expenses = await _expenseService.GetAllAsync();

            incomes = incomes.Where(x => x.UserId == userId).ToList();
            expenses = expenses.Where(x => x.UserId == userId).ToList();

            decimal totalIncome = incomes.Sum(x => x.Amount);
            decimal totalExpense= expenses.Sum(x => x.Amount);
            decimal remaining = totalIncome - totalExpense;

            var apiKey = _configuration["AiSettings:GeminiApiKey"];
            var baseUrl = _configuration["AiSettings:BaseUrl"];
            var model = _configuration["AiSettings:Model"];

            var url = $"{baseUrl}/{model}:generateContent?key={apiKey}";
            var prompt = $"""
            Sen deneyimli bir finans danışmanısın.

            Kullanıcının toplam geliri: {totalIncome} TL

            Toplam gideri: {totalExpense} TL

            Kalan bakiyesi: {remaining} TL

            Giderler:
            """;

           foreach (var expense in expenses)
           {
                  prompt += $"- {expense.Description}: {expense.Amount} TL\n";
           }

           prompt += """

            Lütfen bu bütçeyi analiz et.

            - En fazla 5 maddelik öneri ver.
            - Gereksiz harcamaları belirt.
            - Tasarruf edilebilecek alanları söyle.
            - Cevabını tamamen Türkçe ver.
""";


            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = "Merhaba, SmartBudget uygulaması için çalışıyor musun? Kısa cevap ver."
                    }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

    }
}