using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBI.Core.Cache;

namespace RedisConnectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            //List<Task> list = new List<Task>();
            //for (int i = 0; i < 100000; i++)
            //{
            //    var t = new Task(() =>
            //    {
            //        var a = CacheManager.Get<string>("name");
            //        Console.WriteLine(count++ + a);
            //    });
            //    list.Add(t);
            //    t.Start();
            //}

            //Task.WhenAll(list);
            ParallelLoopResult result = Parallel.For(1, 20000, i =>
            {

                var a = CacheManager.Get<string>("name");
                Console.WriteLine(count++ + a);
            });
            Console.ReadKey();
        }
    }
}