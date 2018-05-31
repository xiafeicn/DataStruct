using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructor
{
    /// <summary>
    /// 直接选择排序
    /// 不稳定的算法
    /// 时间复杂度N*(N-1)/2
    /// </summary>
    public class DirectSort
    {
        public int[] list = new int[] { 14, 31, 9, 25, 11, 68, 77, 3, 55 ,11};

        public void Sort()
        {

            for (int i = 0; i < list.Length - 1; i++)
            {
                int min = i;
                for (int j = i+1; j < list.Length; j++)
                {
                    if (list[j] < list[min])
                    {
                        min = j;
                    }
                }

                int temp = list[min];
                list[min] = list[i];
                list[i] = temp;
            }

            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }
    }
}
