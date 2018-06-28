using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Redis.Service;

namespace MyRedis.BackService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
                Console.WriteLine($"这里是 {tag} 启动了。。");
                using (RedisListService service = new RedisListService())
                {
                    Action act = new Action(() =>
                    {
                        while (true)
                        {
                            var result = service.BlockingPopItemFromLists(new string[] { "test", "task" }, TimeSpan.FromHours(3));
                            Console.WriteLine($"这里是 {tag} 队列获取的消息 {result.Id} {result.Item}");
                        }
                    });
                    act.EndInvoke(act.BeginInvoke(null, null));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }
    }
}
