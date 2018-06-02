using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGeneric.Extend
{
    public class GenericCacheTest
    {
        public static void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(DictionaryCache.GetCache<int>());
                Thread.Sleep(10);
                Console.WriteLine(DictionaryCache.GetCache<long>());
                Thread.Sleep(10);
                Console.WriteLine(DictionaryCache.GetCache<DateTime>());
                Thread.Sleep(10);
                Console.WriteLine(DictionaryCache.GetCache<string>());
                Thread.Sleep(10);
                Console.WriteLine(DictionaryCache.GetCache<GenericCacheTest>());
                Thread.Sleep(10);
            }


            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(GenericCache<int>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<long>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<DateTime>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<string>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(GenericCache<GenericCacheTest>.GetCache());
                Thread.Sleep(10);
            }



        }
    }

    /// <summary>
    /// 字典缓存：静态属性常驻内存
    /// </summary>
    public class DictionaryCache
    {
        private static Dictionary<string, string> _TypeTimeDictionary = null;
        static DictionaryCache()
        {
            Console.WriteLine("This is DictionaryCache 静态构造函数");
            _TypeTimeDictionary = new Dictionary<string, string>();
        }
        public static string GetCache<T>()
        {
            Type type = typeof(T);
            if (!_TypeTimeDictionary.ContainsKey(type.Name))
            {
                _TypeTimeDictionary[type.Name] = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
            }
            return _TypeTimeDictionary[type.Name];
        }
    }


    /// <summary>
    /// 每个不同的T，都会生成一份不同的副本
    /// 适合不同类型，需要缓存一份数据的场景，效率高
    /// 不能清除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericCache<T>
    {
        static GenericCache()
        {
            Console.WriteLine("This is GenericCache 静态构造函数");
            _TypeTime = string.Format("{0}_{1}", typeof(T).FullName, DateTime.Now.ToString("yyyyMMddHHmmss.fff"));
        }

        private static string _TypeTime = "";

        public static string GetCache()
        {
            return _TypeTime;
        }
    }

}
