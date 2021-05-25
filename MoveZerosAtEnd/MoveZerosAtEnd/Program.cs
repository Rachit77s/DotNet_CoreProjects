using System;

namespace MoveZerosAtEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 0,1,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,0 };
            int n = arr.Length;
            PushZerosToEnd(arr, n);
            PushZerosToEndMethodII(arr, n);
            PushZerosAtTheStart(arr, n);
            PrintArray(arr, n);

            Console.ReadKey();
        }

        static void PrintArray(int[] arr, int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");
        }

        //ALGORITHM::
        //Traverse the given array ‘arr’ from left to right.
        //While traversing, maintain count of non-zero elements in array.
        //Let the count be ‘count’.
        //For every non-zero element arr[i], put the element at ‘arr[count]’ and increment ‘count’.
        //After complete traversal, all non-zero elements have already been shifted to front end and ‘count’ is set as index of first 0.
        //Now all we need to do is that run a loop which makes all elements zero from ‘count’ till end of the array.

        // Function which pushes all zeros
        // to end of an array.
        static void PushZerosToEnd(int[] arr, int n)
        {
            // Count of non-zero elements
            int count = 0;

            //{ 1, 9, 8, 4, 0, 0, 2, 7, 0, 6, 0, 9 };

            // Traverse the array. If element encountered is
            // non-zero, then replace the element
            // at index â..countâ.. with this element
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    arr[count] = arr[i];
                    count++;
                }
            }

            // Now all non-zero elements have been shifted to
            // front and â..countâ.. is set as index of first 0.
            // Make all elements 0 from count to end.
            //while (count < n)
            //{
            //    arr[count++] = 0;
            //}

            for (int i = count; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
        }

        //Swapping
        static void PushZerosToEndMethodII(int[] arr, int n)
        {
            //{ 1, 9, 8, 4, 0, 0, 2, 7, 0, 6, 0, 9 };
            // Count of non-zero elements
            int count = 0;
            int temp;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    // Traverse the array. If arr[i] is
                    // non-zero, then swap the element at
                    // index 'count' with the element at index 'i'
                    temp = arr[i];
                    arr[count] = arr[i];
                    arr[i] = temp;
                    //int temp = A[i];
                    //A[i] = A[j];
                    //A[j] = temp;
                    //var buffer = arr[i];
                    //arr[i] = arr[count];
                    //arr[count] = buffer;

                    count = count + 1;
                }
            }
        }

        static void PushZerosAtTheStart(int[] arr, int n)
        {
            ////{ 1, 9, 8, 4, 0, 0, 2, 7, 0, 6, 0, 9 };

            int count = n-1;
            
            for (int i = count; i >= 0; i--)
            {
                if (arr[i] != 0)
                {
                    arr[count--] = arr[i];
                }
            }

            while (count >= 0)
            {
                arr[count] = 0;
                count--;
            }       
        }

        public static void SwapValues<T>(T[] source, long index1, long index2)
        {
            T temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;

            // swap index x and y
            //var buffer = array[x];
            //array[x] = array[y];
            //array[y] = buffer;
        }
    }
}
