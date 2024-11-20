namespace StringProcessorAPI.Logic
{
    public class QuickSortClass
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
}
