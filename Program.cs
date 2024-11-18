using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaximTechC_
{
    internal class Program
    {
        static void Task1()
        {
            string s;
            bool isAllSymbolsAreCorrect = true;
            char[] allowedSymbols = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            List<char> incorrectedSymbols = new List<char>();

            Console.Write("Введите строку: ");
            s = Console.ReadLine();
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
                return;
            }

            if (s.Length % 2 == 1)
            {
                char[] chars = s.ToCharArray();
                Array.Reverse(chars);
                string rev_string = string.Concat(chars);
                string res = string.Concat(rev_string, s);
                Console.WriteLine("Результат: {0}", res);
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

                string res = String.Concat(string.Concat(first_half), string.Concat(second_half));
                Console.WriteLine("Результат: {0}", res);
            }
        }


        static void Main(string[] args)
        {
            // task 1
            Task1();
        }
    }
}
