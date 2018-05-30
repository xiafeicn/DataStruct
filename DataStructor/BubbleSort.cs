using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructor
{
    public class BubbleSort
    {
        public int[] list = new int[] { 14, 31, 9, 25, 11, 68, 77, 3, 55 };

        public void Sort()
        {
            for (int x = 0; x < list.Length - 1; x++)
            {
                for (int y = 0; y < list.Length - x - 1; y++)
                {
                    if (list[y + 1] < list[y])
                    {
                        int temp = list[y];
                        list[y] = list[y + 1];
                        list[y + 1] = temp;
                    }
                }
            }

            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }
    }
}
