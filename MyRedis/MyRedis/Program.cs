using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
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
            
            ParallelLoopResult result = Parallel.For(1, 20000, i =>
            {
                var redis = new RedisStringService();
                count++;
                Console.WriteLine(count);
                redis.Get("name");
            });
            Console.ReadKey();
        }
    }
}
