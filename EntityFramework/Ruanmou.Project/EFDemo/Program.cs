using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using EFDemo.DBFirst;
using EFDemo.CodeFirstDB;
//using EFDemo.CodeFirstFromDB;

namespace EFDemo
{
    /// <summary>
    /// 1 O/RM对象关系映射
    /// 2 EntityFramework DBFirst
    /// 3 EntityFramework codeFirst from db &&codeFirst 
    /// 4 EntityFramework modelFirst
    /// 5 EF的增删改查
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //研究下 code first的注释生成
                Console.WriteLine("欢迎来到.net高级班vip课程，今晚学习O/RM&&EntityFramework");
                //using (DBFirstContext dbContext = new DBFirstContext())
                //{
                //    User user2 = dbContext.Users.Find(2);
                //    User user3 = dbContext.Users.Find(3);

                //    var list = dbContext.Users.ToList();

                //    var list2 = dbContext.Users.Where(u => u.Id < 3);//延迟查询

                //    var list4 = list2.Where(u => u.Name.Length < 3).OrderBy(u => u.Id);

                //    foreach (var item in list4)
                //    {

                //    }

                //    var list3 = dbContext.Users.Where(u => u.Id < 3).ToList();
                //    //new List<int>().Where();
                //    User userNew = new User()
                //    {
                //        Account = "Admin",
                //        State = 0,
                //        CompanyId = 4,
                //        CompanyName = "万达集团",
                //        CreateTime = DateTime.Now,
                //        CreatorId = 1,
                //        Email = "57265177@qq.com",
                //        LastLoginTime = null,
                //        LastModifierId = 0,
                //        LastModifyTime = DateTime.Now,
                //        Mobile = "18664876671",
                //        Name = "yoyo",
                //        Password = "12356789",
                //        UserType = 1
                //    };
                //    dbContext.Users.Add(userNew);
                //    dbContext.SaveChanges();

                //    userNew.Name = "安德鲁";
                //    dbContext.SaveChanges();

                //    dbContext.Users.Remove(userNew);
                //    dbContext.SaveChanges();
                //}
                using (CodeFirstDBContext dbContext = new CodeFirstDBContext())
                {

                    {
                        User user2 = dbContext.User.Find(2);
                        User user3 = dbContext.User.Find(3);

                        var list = dbContext.User.ToList();

                        var list2 = dbContext.User.Where(u => u.Id < 3);//延迟查询

                        var list4 = list2.Where(u => u.Name.Length < 3).OrderBy(u => u.Id);

                        foreach (var item in list4)
                        {

                        }

                        var list3 = dbContext.User.Where(u => u.Id < 3).ToList();
                        //new List<int>().Where();
                        User userNew = new User()
                        {
                            Account = "Admin",
                            State = 0,
                            CompanyId = 4,
                            CompanyName = "万达集团",
                            CreateTime = DateTime.Now,
                            CreatorId = 1,
                            Email = "57265177@qq.com",
                            LastLoginTime = null,
                            LastModifierId = 0,
                            LastModifyTime = DateTime.Now,
                            Mobile = "18664876671",
                            Name = "yoyo",
                            Password = "12356789",
                            UserType = 1
                        };
                        dbContext.User.Add(userNew);
                        dbContext.SaveChanges();

                        userNew.Name = "安德鲁";
                        dbContext.SaveChanges();

                        dbContext.User.Remove(userNew);
                        dbContext.SaveChanges();
                    }

                    {
                        var list = dbContext.User.Where(u => new int[] { 1, 2, 3, 5, 7, 8, 9, 10, 11, 12, 14 }.Contains(u.Id));

                        list = list.Where(v => v.Id < 5);
                        list = list.OrderBy(v => v.Id);

                        foreach (var user in list)
                        {
                            Console.WriteLine(user.Name);
                        }
                    }
                    {
                        var list = from u in dbContext.User
                                   where new int[] { 1, 2, 3, 5, 7, 8, 9, 10, 11, 12, 14 }.Contains(u.Id)
                                   select u;

                        foreach (var user in list)
                        {
                            Console.WriteLine(user.Name);
                        }
                    }
                    {
                        var list = dbContext.User.Where(u => new int[] { 1, 2, 3, 5, 7, 8, 9, 10, 11, 12, 14 }.Contains(u.Id))
                                                  .Select(u => new
                                                  {
                                                      Account = u.Account,
                                                      Pwd = u.Password
                                                  }).Skip(3).Take(5);
                        foreach (var user in list)
                        {
                            Console.WriteLine(user.Pwd);
                        }
                    }
                    {
                        var list = (from u in dbContext.User
                                    where new int[] { 1, 2, 3, 5, 7, 8, 9, 10, 11, 12, 14 }.Contains(u.Id)
                                    select new
                                    {
                                        Account = u.Account,
                                        Pwd = u.Password
                                    }).Skip(3).Take(5);

                        foreach (var user in list)
                        {
                            Console.WriteLine(user.Account);
                        }
                    }

                    {
                        var list = dbContext.User.Where(u => u.Name.StartsWith("小") && u.Name.EndsWith("新"))
                                                   .Where(u => u.Name.EndsWith("新"))
                                                   .Where(u => u.Name.Contains("小新"))
                                                   .Where(u => u.Name.Length < 5)
                                                   .OrderBy(u => u.Id);

                        foreach (var user in list)
                        {
                            Console.WriteLine(user.Name);
                        }
                    }
                    {
                        var list = from u in dbContext.User
                                   join c in dbContext.Company on u.CompanyId equals c.Id
                                   where new int[] { 1, 2, 3, 4, 6, 7, 10 }.Contains(u.Id)
                                   select new
                                   {
                                       Account = u.Account,
                                       Pwd = u.Password,
                                       CompanyName = c.Name
                                   };
                        foreach (var user in list)
                        {
                            Console.WriteLine("{0} {1}", user.Account, user.Pwd);
                        }
                    }
                    {
                        var list = from u in dbContext.User
                                   join c in dbContext.Category on u.CompanyId equals c.Id
                                   into ucList
                                   from uc in ucList.DefaultIfEmpty()
                                   where new int[] { 1, 2, 3, 4, 6, 7, 10 }.Contains(u.Id)
                                   select new
                                   {
                                       Account = u.Account,
                                       Pwd = u.Password
                                   };
                        foreach (var user in list)
                        {
                            Console.WriteLine("{0} {1}", user.Account, user.Pwd);
                        }
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
