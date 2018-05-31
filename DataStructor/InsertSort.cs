using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructor
{
    public class InsertSort
    {
        public int[] list = new int[] { 14, 31, 9, 25, 11, 68, 77, 3, 55 };

        public void InsertionSort()
        {
            if (list.Length < 2)
            {
                return;
            }

            for (int i = 0; i < list.Length - 1; i++)
            {

                int j = i + 1;
                int temp = list[j];
                for (; j > 0; j--)
                {
                    if (list[j - 1] > temp)
                    {
                        list[j] = list[j - 1];
                    }
                    else
                    {
                        break;
                    }
                }
                list[j] = temp;
            }

            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }
    }
}
