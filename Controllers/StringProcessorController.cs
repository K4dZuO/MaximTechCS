using Microsoft.AspNetCore.Mvc;
using StringProcessorAPI.Logic;

[ApiController]
[Route("api/[controller]")]
public class StringProcessorController : ControllerBase
{
    private readonly Program _processor;

    public StringProcessorController()
    {
        // Используем существующий класс Program
        _processor = new Program();
    }

    [HttpGet("process")]
    public IActionResult ProcessString([FromQuery] string input, [FromQuery] int sortMethod)
    {
        if (string.IsNullOrEmpty(input))
        {
            return BadRequest(new { Error = "Входящая строка пуста." });
        }

        // Сбор некорректных символов
        var invalidChars = input.Where(c => c < 'a' || c > 'z').Distinct().ToList();

        if (invalidChars.Any())
        {
            return BadRequest(new
            {
                Error = "Входящая строка содержит некорректные символы.",
                InvalidCharacters = invalidChars
            });
        }

        // Обработка строки
        string processedString = StringProcessorClass.ProcessString(input);

        if (processedString == "Error")
        {
            return BadRequest(new { Error = "Некорректный ввод. Ошибка при обработке строки." });
        }

        // Выбор метода сортировки
        string sortedString;
        if (sortMethod == 1)
        {
            sortedString = SortQuick(processedString);
        }
        else if (sortMethod == 2)
        {
            sortedString = SortTree(processedString);
        }
        else
        {
            return BadRequest(new { Error = "Некорректный выбор сортировки. Необходимо 1 для QuickSort или 2 для TreeSort." });
        }

        // Формируем результат
        return Ok(new
        {
            ProcessedString = processedString,
            CharFrequency = StringProcessorClass.GetDensityChars(processedString),
            LongestSubstring = StringProcessorClass.GetLongestSequence(processedString),
            SortedString = sortedString,
            TruncatedString = Truncate(processedString)
        });
    }


    // Реализация сортировок
    private string SortQuick(string input)
    {
        int[] asciiCodes = input.Select(c => (int)c).ToArray();
        var quickSortExecutor = new QuickSortClass();
        int[] sortedAsciiCodes = quickSortExecutor.QuickSort(asciiCodes);
        return new string(sortedAsciiCodes.Select(code => (char)code).ToArray());
    }

    private string SortTree(string input)
    {
        int[] asciiCodes = input.Select(c => (int)c).ToArray();
        int[] sortedAsciiCodes = TreeSortClass.TreeSort(asciiCodes);
        return new string(sortedAsciiCodes.Select(code => (char)code).ToArray());
    }

    private string Truncate(string input)
    {
        int indexToRemove = APICLass.GetRandomNumberAsync(input.Length).GetAwaiter().GetResult();
        return input.Remove(indexToRemove, 1);
    }
}
