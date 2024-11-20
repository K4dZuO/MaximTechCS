using System.Text.Json;

namespace StringProcessorAPI.Logic
{
    public class APICLass
    {
        private static string GetAPIKey()
        {
            // Создаем конфигурацию
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Указываем текущую директорию
                .AddJsonFile("F:\\K4dZuO\\Important for studying\\MaximTech\\StringProcessorAPI\\config.json", optional: false, reloadOnChange: true) // Подключаем JSON-файл
                .Build();

            string apiKey = config["ApiKey"];
            return apiKey;
        }

        public static async Task<int> GetRandomNumberAsync(int max)
        {
            string apiKey = GetAPIKey(); // Получаем API-ключ
            string url = "https://api.random.org/json-rpc/4/invoke";

            // Формируем JSON-запрос
            string jsonRequest = $@"
        {{
            ""jsonrpc"": ""2.0"",
            ""method"": ""generateIntegers"",
            ""params"": {{
                ""apiKey"": ""{apiKey}"",
                ""n"": 1,
                ""min"": 0,
                ""max"": {max - 1}
            }},
            ""id"": 1
        }}";

            using HttpClient client = new HttpClient();
            try
            {
                // Отправляем запрос
                HttpResponseMessage response = await client.PostAsync(url,
                    new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode(); // Выбрасывает исключение, если статус-код неуспешный

                string responseContent = await response.Content.ReadAsStringAsync();

                // Обработка ответа
                var result = JsonSerializer.Deserialize<JsonElement>(responseContent);
                int randomNumber = result.GetProperty("result")
                                          .GetProperty("random")
                                          .GetProperty("data")[0]
                                          .GetInt32();

                return randomNumber; // Возвращаем случайное число
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"\nОшибка при запросе к API: {ex.Message}");

                // Генерация случайного числа средствами .NET в случае ошибки
                Random random = new Random();
                return random.Next(0, max); // Генерируем случайное число от 0 до 100
            }
        }
    }
}
