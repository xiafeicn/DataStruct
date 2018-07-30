using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRedisDemo
{
    ///<summary>
    /// 1 Cache和NoSql
    /// 2 String
    /// 3 Hashtable
    /// 4 Set
    /// 5 ZSet
    /// 6 List
    /// 7 分布式异步队列
    /// 
    /// REmote DIctionary Server
    /// ServiceStack.Redis  ado.net
    /// 6000 Request per hour
    ///</summary>
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            Stopwatch sp = new Stopwatch();
            long costTime = 0;
            sp.Start();
            ParallelLoopResult result = Parallel.For(1, 20000, i =>
            {
                count++;
                sp.Reset();
                sp.Start();
                new RedisStringService().Get("name");
                Console.WriteLine(count + "耗时{0}毫秒", sp.ElapsedMilliseconds);
                costTime += sp.ElapsedMilliseconds;
                sp.Stop();
            });
            sp.Stop();
            Console.WriteLine("耗时{0}ms", costTime);
            Console.ReadKey();
        }
    }


}
