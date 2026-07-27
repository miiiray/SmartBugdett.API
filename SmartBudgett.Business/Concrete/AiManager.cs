using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;

namespace SmartBudgett.Business.Concrete.Managers
{
    public class AiManager : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;

        public AiManager(
            HttpClient httpClient,
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
            // 1. Gelir ve gider verilerini servislerden alıyoruz.
            var incomes = await _incomeService.GetAllAsync();
            var expenses = await _expenseService.GetAllAsync();

            // Sadece giriş yapan kullanıcıya ait kayıtları seçiyoruz.
            incomes = incomes
                .Where(x => x.UserId == userId)
                .ToList();

            expenses = expenses
                .Where(x => x.UserId == userId)
                .ToList();

            if (!incomes.Any() && !expenses.Any())
            {
                return "Bütçe analizi yapılabilmesi için önce gelir veya gider eklemelisiniz.";
            }

            // 3. Toplam değerleri hesaplıyoruz.
            decimal totalIncome = incomes.Sum(x => x.Amount);
            decimal totalExpense = expenses.Sum(x => x.Amount);
            decimal remaining = totalIncome - totalExpense;

            // 4. Kullanıcının hiç verisi yoksa API isteği göndermiyoruz.
            if (!incomes.Any() && !expenses.Any())
            {
                return "Bütçe analizi yapılabilmesi için önce gelir veya gider eklemelisiniz.";
            }

            // 5. OpenAI ayarlarını alıyoruz.
            var apiKey = _configuration["AiSettings:OpenAIApiKey"];
            var baseUrl = _configuration["AiSettings:BaseUrl"];
            var model = _configuration["AiSettings:Model"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "OpenAI API anahtarı bulunamadı. User Secrets ayarını kontrol edin.");
            }

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException(
                    "OpenAI BaseUrl ayarı bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new InvalidOperationException(
                    "OpenAI model ayarı bulunamadı.");
            }

            // 6. Yapay zekâya gönderilecek bütçe açıklamasını oluşturuyoruz.
            var promptBuilder = new StringBuilder();

            promptBuilder.AppendLine("Sen deneyimli bir finans danışmanısın.");
            promptBuilder.AppendLine();
            promptBuilder.AppendLine($"Kullanıcının toplam geliri: {totalIncome:N2} TL");
            promptBuilder.AppendLine($"Kullanıcının toplam gideri: {totalExpense:N2} TL");
            promptBuilder.AppendLine($"Kullanıcının kalan bakiyesi: {remaining:N2} TL");
            promptBuilder.AppendLine();

            promptBuilder.AppendLine("Gelirler:");

            if (incomes.Any())
            {
                foreach (var income in incomes)
                {
                    promptBuilder.AppendLine(
                        $"- {income.Description}: {income.Amount:N2} TL");
                }
            }
            else
            {
                promptBuilder.AppendLine("- Kayıtlı gelir bulunmuyor.");
            }

            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Giderler:");

            if (expenses.Any())
            {
                foreach (var expense in expenses)
                {
                    promptBuilder.AppendLine(
                        $"- {expense.Description}: {expense.Amount:N2} TL");
                }
            }
            else
            {
                promptBuilder.AppendLine("- Kayıtlı gider bulunmuyor.");
            }

            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Bu bütçeyi analiz et.");
            promptBuilder.AppendLine("- En fazla 5 maddelik öneri ver.");
            promptBuilder.AppendLine("- Gereksiz olabilecek harcamaları belirt.");
            promptBuilder.AppendLine("- Tasarruf edilebilecek alanları söyle.");
            promptBuilder.AppendLine("- Kullanıcının bütçe durumunu kısaca değerlendir.");
            promptBuilder.AppendLine("- Kesin yatırım tavsiyesi verme.");
            promptBuilder.AppendLine("- Cevabını tamamen Türkçe ver.");

            var prompt = promptBuilder.ToString();

            // 7. OpenAI Responses API için istek gövdesini oluşturuyoruz.
            var requestBody = new
            {
                model = model,
                input = prompt,
                max_output_tokens = 700
            };

            var json = JsonSerializer.Serialize(requestBody);

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{baseUrl.TrimEnd('/')}/responses");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // 8. İsteği OpenAI API'ye gönderiyoruz.
            using var response = await _httpClient.SendAsync(request);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            // 9. Başarısız cevapları anlaşılır bir hata olarak döndürüyoruz.
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"OpenAI API hatası ({(int)response.StatusCode}): " +
                    responseContent);
            }

            // 10. Dönen JSON içerisinden yalnızca AI metnini çıkarıyoruz.
            return ExtractResponseText(responseContent);
        }

        private static string ExtractResponseText(string responseContent)
        {
            using var document = JsonDocument.Parse(responseContent);

            var root = document.RootElement;

            if (!root.TryGetProperty("output", out var outputArray))
            {
                return "OpenAI yanıtı alındı ancak analiz metni bulunamadı.";
            }

            foreach (var outputItem in outputArray.EnumerateArray())
            {
                if (!outputItem.TryGetProperty(
                        "content",
                        out var contentArray))
                {
                    continue;
                }

                foreach (var contentItem in contentArray.EnumerateArray())
                {
                    if (contentItem.TryGetProperty(
                            "text",
                            out var textElement))
                    {
                        var text = textElement.GetString();

                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            return text;
                        }
                    }
                }
            }

            return "OpenAI yanıtı alındı ancak analiz metni bulunamadı.";
        }
    }
}