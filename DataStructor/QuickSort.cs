using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructor
{
    public class QuickSortClass
    {
        public int[] list = new int[] { 14, 31, 9, 25, 11, 68, 77, 3, 55 };

        public void Sort()
        {
            QuickSort(list, 0, list.Length - 1);

            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }

        public void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int position = Division(array, left, right);

                QuickSort(array, left, position - 1);

                QuickSort(array, position + 1, right);
            }
        }

        //public int Division(int[] array, int left, int right)
        //{
        //    int baseNum = array[left];  //取最左值做key

        //    while (left < right)
        //    {
        //        //从数组的右端开始向前找，一直找到比base小的数字为止(包括base同等数)  
        //        while (left < right && array[right] >= baseNum)
        //            right = right - 1;

        //        //最终找到了比baseNum小的元素，要做的事情就是此元素放到base的位置  
        //        array[left] = array[right];

        //        //从数组的左端开始向后找，一直找到比base大的数字为止（包括base同等数）  
        //        while (left < right && array[left] <= baseNum)
        //            left = left + 1;

        //        //最终找到了比baseNum大的元素，要做的事情就是将此元素放到最后的位置  
        //        array[right] = array[left];
        //    }
        //    //最后就是把baseNum放到该left的位置  
        //    array[left] = baseNum;
        //    //最终，我们发现left位置的左侧数值部分比left小，left位置右侧数值比left大  
        //    //至此，我们完成了第一篇排序  
        //    return left;
        //}


        public int Division(int[] array, int left, int right)
        {
            int baseNum = array[left];

            while (left < right)
            {
                while (left < right && array[right] > baseNum)
                {
                    right--;
                }
                array[left] = array[right];
                while (left < right && array[left] < baseNum)
                {
                    left++;
                }
                array[right] = array[left];
            }
            array[left] = baseNum;
            return left;
        }
    }
}
