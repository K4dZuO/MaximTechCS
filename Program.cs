using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaximTechC_
{

    class Node
    {
        public int Key;
        public Node Left;
        public Node Right;

        public Node(int key)
        {
            Key = key;
            Left = null;
            Right = null;
        }
    }

    class BinaryTree
    {
        public Node Root;

        public BinaryTree()
        {
            Root = null;
        }

        public void Add(int key)
        {
            Node node = new Node(key);

            if (Root == null)
            {
                Root = node;
            }
            else
            {
                RecursiveAdd(Root, node);
            }
        }

        private void RecursiveAdd(Node currentNode, Node addNode)
        {
            if (addNode.Key < currentNode.Key)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = addNode;
                }
                else
                {
                    RecursiveAdd(currentNode.Left, addNode);
                }
            }
            else if (addNode.Key >= currentNode.Key)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = addNode;
                }
                else
                {
                    RecursiveAdd(currentNode.Right, addNode);
                }
            }
        }

        public void InfinixOrder(Node Root, List<int> sortedList)
        {
            if (Root == null)
            {
                return;
            }

            InfinixOrder(Root.Left, sortedList);
            sortedList.Add(Root.Key);
            InfinixOrder(Root.Right, sortedList);
        }

    }

    public class TreeSortClass
    {
        public static int[] TreeSort(int[] array)
        {
            BinaryTree binaryTree = new BinaryTree();

            foreach (int item in array)
            {
                binaryTree.Add(item);
            }

            List<int> sortedList = new List<int>();
            binaryTree.InfinixOrder(binaryTree.Root, sortedList);

            return sortedList.ToArray();
        }
    }

    class QuickSortClass
    {

        public int[] QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            int pivotIndex = GetPivotIndex(array, minIndex, maxIndex);

            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public int[] QuickSort(int[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }

        private int GetPivotIndex(int[] array, int minIndex, int maxIndex)
        {
            int pivotIndex = minIndex - 1;

            for (int i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivotIndex++;
                    Swap(ref array[pivotIndex], ref array[i]);
                }
            }

            pivotIndex++;
            Swap(ref array[pivotIndex], ref array[maxIndex]);

            return pivotIndex;
        }

        private void Swap(ref int leftItem, ref int rightItem)
        {
            int temp = leftItem;
            leftItem = rightItem;
            rightItem = temp;
        }
    }

    class Program
    {
        private Dictionary<string, int> GetDensityChars(string s)
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
                    dictSymbols[charString]++ ;
                }
            }
            return dictSymbols;
        }

        private string ReadDictionary(Dictionary<string, int> dictSymbols)
        {
            string res = "";
            foreach (string stringKey in dictSymbols.Keys)
            {
                res += $"{stringKey} : {dictSymbols[stringKey]}\n";
            }
            return res;
        }

        private string GetLongestSequence(string s)
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
                for (int i = s.Length - 1; i > 0; i--)
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

        private string ProcessString()
        {
            string s;
            string res;
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

        static void Main(string[] args)
        {
            //task 1, 2
            Program program = new Program();

            string res = program.ProcessString();
            if (res == "Error")
            {
                return;
            }
            Console.WriteLine("Результат: {0}", res); 

            //task3
            Dictionary<string, int> dctDensity = program.GetDensityChars(res);
            string dictInfo = program.ReadDictionary(dctDensity);
            Console.WriteLine("Частота встречаемости символов в обработанной строке:");
            Console.WriteLine(dictInfo);


            // task 4
            Console.WriteLine("Самая длинная подстрока: " + program.GetLongestSequence(res));


            //task5
            int[] asciiCodes = new int[res.Length];
            int[] sortedAsciiCodes;

            for (int i = 0; i < res.Length; i++)
            {
                asciiCodes[i] = Convert.ToByte(res[i]);
            }

            Console.WriteLine("Каким образом вы хотите отсортировать строку? Введите одну цифру: \n 1 - Быстрая сортировка (Quick Sort)\n 2 - Сортировка Деревом (Tree Sort)");
            char inputedChar = Console.ReadKey().KeyChar;
            char[] allowedAndswers = new char[] { '1', '2' };

            while (!allowedAndswers.Contains(inputedChar))
            {
                Console.WriteLine("\nОшибка! Попробуйте еще раз!");
                inputedChar = Console.ReadKey().KeyChar;
            }

            if (inputedChar == allowedAndswers[0])
            {
                QuickSortClass quickSortExecutor = new QuickSortClass();
                sortedAsciiCodes = quickSortExecutor.QuickSort(asciiCodes);
            }
            else if (inputedChar == allowedAndswers[1]) 
            {
                sortedAsciiCodes = TreeSortClass.TreeSort(asciiCodes);
            }
            else
            {
                sortedAsciiCodes = new int[] { 1, 2, 3, 4 };
            }

            Console.Write("\nОтсортированная строка: ");
            foreach (int code in sortedAsciiCodes)
            {
                Console.Write(Convert.ToChar(code));
            }
        }
    }
}
