namespace StringProcessorAPI.Logic
{
    public class StringProcessorClass
    {
        public static Dictionary<string, int> GetDensityChars(string s)
        {
            Dictionary<string, int> dictSymbols = new Dictionary<string, int>();

            foreach (char c in s)
            {
                string charString = Convert.ToString(c);
                if (!dictSymbols.ContainsKey(charString))
                {
                    dictSymbols.Add(charString, 1);
                }
                else
                {
                    dictSymbols[charString]++;
                }
            }
            return dictSymbols;
        }

        public static string ReadDictionary(Dictionary<string, int> dictSymbols)
        {
            string res = "";
            foreach (string stringKey in dictSymbols.Keys)
            {
                res += $"{stringKey} : {dictSymbols[stringKey]}\n";
            }
            return res;
        }

        public static string GetLongestSequence(string s)
        {
            string res;
            int startIndex = -1;
            int endIndex = s.Length - 1;
            char[] allowedChars = "aeiouy".ToCharArray();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (allowedChars.Contains(c))
                {
                    startIndex = i;
                    break;
                }
            }

            if (startIndex == -1)
            {
                return "";
            }
            else
            {
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    char c = s[i];
                    if (allowedChars.Contains(c))
                    {
                        endIndex = i;
                        break;
                    }
                }
                res = s.Substring(startIndex, endIndex - startIndex + 1);
            }
            return res;
        }

        public static string ProcessString(string s)
        {
            string res;
            bool isAllSymbolsAreCorrect = true;
            char[] allowedSymbols = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            List<char> incorrectedSymbols = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!allowedSymbols.Contains(s[i]))
                {
                    isAllSymbolsAreCorrect = false;
                    incorrectedSymbols.Add(s[i]);
                }
            }

            if (!isAllSymbolsAreCorrect)
            {
                Console.WriteLine($"Ошибка! Введены некорректные символы: {string.Join(", ", incorrectedSymbols)}");
                return "Error";
            }

            if (s.Length % 2 == 1)
            {
                char[] chars = s.ToCharArray();
                Array.Reverse(chars);
                string rev_string = string.Concat(chars);
                res = string.Concat(rev_string, s);
            }
            else
            {
                int middle_index = s.Length / 2;
                char[] chars = s.ToCharArray();
                char[] first_half = new char[middle_index];
                char[] second_half = new char[middle_index];

                Array.Copy(chars, 0, first_half, 0, middle_index);
                Array.Reverse(first_half);

                Array.Copy(chars, middle_index, second_half, 0, middle_index);
                Array.Reverse(second_half);

                res = String.Concat(string.Concat(first_half), string.Concat(second_half));
            }

            return res;
        }
    }
}
