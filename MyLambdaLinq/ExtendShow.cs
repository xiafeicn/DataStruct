using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLambdaLinq.Extend
{
    /// <summary>
    /// 扩展方法：就是在不修改类型封装前提下，给类型额外的扩展一个方法
    /// 
    /// 不能滥用,尤其是一些基类型
    /// </summary>
    public static class ExtendShow
    {
        /// <summary>
        /// 扩展方法：静态类里的静态方法，第一个参数类型前面加上this
        /// </summary>
        /// <param name="iParameter"></param>
        /// <returns></returns>
        public static int ToInt(this int? iParameter)
        {
            return iParameter ?? -1;
        }


        public static void ExtendLambdaShow(this LambdaShow lambda, int iParameter, string sParameter)
        {
            Console.WriteLine($"LambdaShow=={iParameter}==={sParameter}");
        }
        /// <summary>
        /// 如果跟实例方法相同，优先实例方法
        /// </summary>
        /// <param name="lambda"></param>
        public static void Show(this LambdaShow lambda)
        {
            Console.WriteLine($"LambdaShow");
        }



        public static void DoNothing(this object oParameter)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ElevenWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new Exception("source");
            }
            if (predicate == null)
            {
                throw new Exception("predicate");
            }

            //常规情况下数据过滤
            List<TSource> studentListLessThan30 = new List<TSource>();
            foreach (var student in source)
            {
                //if (item.Age < 30)
                if (predicate(student))
                {
                    studentListLessThan30.Add(student);
                }
            }

            return studentListLessThan30;


        }


    }
}
