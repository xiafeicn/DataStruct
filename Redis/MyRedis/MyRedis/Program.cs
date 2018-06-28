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
            try
            {
                Student student_1 = new Student()
                {
                    Id = 11,
                    Name = "Eleven"
                };
                Student student_2 = new Student()
                {
                    Id = 12,
                    Name = "Twelve",
                    Remark = "123423245"
                };


                Console.WriteLine("*****************************************");
                {

                    //using (RedisStringService service = new RedisStringService())
                    //{
                    //    service.FlushAll();
                    //    service.Set("RedisStringService_key1", "RedisStringService_value1");
                    //    Console.WriteLine(service.Get("RedisStringService_key1"));
                    //    Console.WriteLine(service.GetAndSetValue("RedisStringService_key1", "RedisStringService_value2"));
                    //    Console.WriteLine(service.Get("RedisStringService_key1"));

                    //    service.Append("RedisStringService_key1", "Append");
                    //    Console.WriteLine(service.Get("RedisStringService_key1"));
                    //    service.Set("RedisStringService_key1", "RedisStringService_value", DateTime.Now.AddSeconds(5));
                    //    Console.WriteLine(service.Get("RedisStringService_key1"));
                    //    Thread.Sleep(5000);
                    //    Console.WriteLine(service.Get("RedisStringService_key1"));
                    //}

                }

                //保存 查询对象：
                //Student_2_id  12  Student_2_Name  Twelve
                // 序列化后保存一个对象没问题，
                //查询--反序列化--修改--序列化--保存

                Console.WriteLine("*****************************************");
                {
                    //using (RedisHashService service = new RedisHashService())
                    //{
                    //    service.FlushAll();
                    //    service.SetEntryInHash("lisi", "id", "15");

                    //    service.SetEntryInHash("zhangsan", "id", "13");
                    //    service.SetEntryInHash("zhangsan", "Name", "Thirteen");
                    //    service.SetEntryInHashIfNotExists("zhangsan", "Remark", "1234567");
                    //    var value13 = service.GetHashValues("zhangsan");
                    //    var key13 = service.GetHashKeys("zhangsan");

                    //    var dicList = service.GetAllEntriesFromHash("zhangsan");

                    //    service.SetEntryInHash("zhangsan", "id", "14");//同一条数据，覆盖
                    //    service.SetEntryInHash("zhangsan", "Name", "Fourteen");
                    //    service.SetEntryInHashIfNotExists("zhangsan", "Remark", "2345678");//同一条数据，不覆盖
                    //    service.SetEntryInHashIfNotExists("zhangsan", "Other", "234543");//没有数据就添加
                    //    service.SetEntryInHashIfNotExists("zhangsan", "OtherField", "1235665");


                    //    var value14 = service.GetHashValues("zhangsan");
                    //    service.RemoveEntryFromHash("zhangsan", "Remark");
                    //    service.SetEntryInHashIfNotExists("zhangsan", "Remark", "2345678");
                    //    value14 = service.GetHashValues("zhangsan");

                    //    //service.StoreAsHash<Student>(student_1);
                    //    //Student student1 = service.GetFromHash<Student>(11);
                    //    //service.StoreAsHash<Student>(student_2);
                    //    //Student student2 = service.GetFromHash<Student>(12);
                    //}
                }
                Console.WriteLine("*****************************************");
                {
                    using (RedisSetService service = new RedisSetService())
                    {
                        //key--values
                        service.FlushAll();
                        service.Add("Advanced", "111");
                        service.Add("Advanced", "112");
                        service.Add("Advanced", "113");
                        service.Add("Advanced", "115");
                        service.Add("Advanced", "114");
                        service.Add("Advanced", "111");

                        service.Add("Begin", "111");
                        service.Add("Begin", "112");
                        service.Add("Begin", "113");
                        service.Add("Begin", "117");
                        service.Add("Begin", "116");
                        service.Add("Begin", "111");

                        service.Add("Internal", "111");
                        service.Add("Internal", "112");
                        service.Add("Internal", "117");
                        service.Add("Internal", "119");
                        service.Add("Internal", "118");
                        service.Add("Internal", "111");

                        var result = service.GetAllItemsFromSet("Advanced");
                        var result2 = service.GetRandomItemFromSet("Advanced");
                        result = service.GetAllItemsFromSet("Begin");
                        result2 = service.GetRandomItemFromSet("Begin");

                        var result3 = service.GetIntersectFromSets("Advanced", "Begin");//交
                        result3 = service.GetDifferencesFromSet("Advanced", "Begin", "Internal");//差
                        result3 = service.GetUnionFromSets("Advanced", "Begin", "Internal");//并

                        service.RemoveItemFromSet("Advanced", "111");
                        result = service.GetAllItemsFromSet("Advanced");
                        service.RandomRemoveItemFromSet("Advanced");
                        result = service.GetAllItemsFromSet("Advanced");
                    }
                }
                Console.WriteLine("*****************************************");
                {
                    using (RedisZSetService service = new RedisZSetService())
                    {
                        service.FlushAll();
                        service.Add("score", "111");
                        service.Add("score", "112");
                        service.Add("score", "113");
                        service.Add("score", "114");
                        service.Add("score", "115");
                        service.Add("score", "111");

                        service.AddItemToSortedSet("user", "Eleven1", 1);
                        service.AddItemToSortedSet("user", "Eleven2", 2);
                        service.AddItemToSortedSet("user", "Eleven3", 5);
                        service.AddItemToSortedSet("user", "Eleven4", 3);
                        service.AddItemToSortedSet("user", "1Eleven2", 4);


                        var list = service.GetAll("score");
                        var listDesc = service.GetAllDesc("score");

                        var user = service.GetAll("user");
                        var userDesc = service.GetAllDesc("user");
                    }
                }
                Console.WriteLine("*****************************************");
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.FlushAll();

                        List<string> stringList = new List<string>();
                        for (int i = 0; i < 10; i++)
                        {
                            stringList.Add(string.Format($"放入任务{i}"));
                        }

                        service.LPush("test", "这是一个学生1");
                        service.LPush("test", "这是一个学生2");
                        service.LPush("test", "这是一个学生3");
                        service.LPush("test", "这是一个学生4");
                        service.LPush("test", "这是一个学生5");
                        service.LPush("test", "这是一个学生6");
                        service.Add("task", stringList);

                        Console.WriteLine(service.Count("test"));
                        Console.WriteLine(service.Count("task"));
                        var list = service.Get("test");
                        list = service.Get("task", 2, 4);

                        Action act = new Action(() =>
                         {
                             while (true)
                             {
                                 Console.WriteLine("************请输入数据**************");
                                 string testTask = Console.ReadLine();
                                 service.LPush("test", testTask);
                             }
                         });
                        act.EndInvoke(act.BeginInvoke(null, null));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
