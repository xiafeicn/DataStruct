using MyLambdaLinq.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLambdaLinq
{
    public class LinqShow
    {
        /// <summary>
        /// 准备数据
        /// </summary>
        /// <returns></returns>
        private List<Student> GetStudentList()
        {
            #region 初始化数据
            List<Student> studentList = new List<Student>()
            {
                new Student()
                {
                    Id=1,
                    Name="打兔子的猎人",
                    ClassId=2,
                    Age=35
                },
                new Student()
                {
                    Id=1,
                    Name="Alpha Go",
                    ClassId=2,
                    Age=23
                },
                 new Student()
                {
                    Id=1,
                    Name="白开水",
                    ClassId=2,
                    Age=27
                },
                 new Student()
                {
                    Id=1,
                    Name="狼牙道",
                    ClassId=2,
                    Age=26
                },
                new Student()
                {
                    Id=1,
                    Name="Nine",
                    ClassId=2,
                    Age=25
                },
                new Student()
                {
                    Id=1,
                    Name="Y",
                    ClassId=2,
                    Age=24
                },
                new Student()
                {
                    Id=1,
                    Name="小昶",
                    ClassId=2,
                    Age=21
                },
                 new Student()
                {
                    Id=1,
                    Name="yoyo",
                    ClassId=2,
                    Age=22
                },
                 new Student()
                {
                    Id=1,
                    Name="冰亮",
                    ClassId=2,
                    Age=34
                },
                 new Student()
                {
                    Id=1,
                    Name="瀚",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="毕帆",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="一点半",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="小石头",
                    ClassId=2,
                    Age=28
                },
                new Student()
                {
                    Id=1,
                    Name="大海",
                    ClassId=2,
                    Age=30
                },
                 new Student()
                {
                    Id=3,
                    Name="yoyo",
                    ClassId=3,
                    Age=30
                },
                  new Student()
                {
                    Id=4,
                    Name="unknown",
                    ClassId=4,
                    Age=30
                }
            };
            #endregion
            return studentList;
        }


        public void Show()
        {
            List<Student> studentList = this.GetStudentList();


            //常规情况下数据过滤
            List<Student> studentListLessThan30 = new List<Student>();
            foreach (var item in studentList)
            {
                if (item.Age < 30)
                {
                    studentListLessThan30.Add(item);
                }
            }

            //studentListLessThan30

            {
                //Func<Student, bool> predicate = s => s.Age < 30;
                //var list = studentList.Where<Student>(predicate);//linq

                //var list = Enumerable.Where(studentList, s => s.Age < 30);
                var list = studentList.Where<Student>(s =>
                {
                    Console.WriteLine("1234678");//迭代器的延迟
                    return s.Age < 30;
                });//linq  延迟

                //var list = studentList.Where<Student>(s =>
                //{
                //    Console.WriteLine("1234678");//迭代器的延迟
                //    return s.Age < 30;
                //}).ToList();//linq  非延迟
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name} Age={item.Age}");
                }
                //linq to object
            }
            //Queryable

            {
                var list = studentList.ElevenWhere<Student>(s =>
                {
                    Console.WriteLine("1234678");
                    return s.Age < 30;
                });//linq
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name} Age={item.Age}");
                }
                //linq to object
            }
            {
                var list = studentList.ElevenWhere<Student>(s => s.Name.Length > 3);//linq
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name} Age={item.Age}");
                }
                //linq to object
            }








            {
                //查询运算符
                var list = studentList.Where<Student>(s => s.Age < 30 && s.Name.Length > 3);//陈述句
                foreach (var item in list)
                {
                    Console.WriteLine("Name={0}  Age={1}", item.Name, item.Age);
                }
            }

            {
                //查询表达式
                Console.WriteLine("********************");
                var list = from s in studentList
                           where s.Age < 30 && s.Name.Length > 3
                           select s;

                foreach (var item in list)
                {
                    Console.WriteLine("Name={0}  Age={1}", item.Name, item.Age);
                }
            }

            #region linq to object Show
            {
                Console.WriteLine("********************");
                var list = studentList.Where<Student>(s => s.Age < 30)
                                     .Select(s => new
                                     {
                                         IdName = s.Id + s.Name,
                                         ClassName = s.ClassId == 2 ? "高级班" : "其他班"
                                     });
                foreach (var item in list)
                {
                    Console.WriteLine("Name={0}  Age={1}", item.ClassName, item.IdName);
                }
            }
            {
                Console.WriteLine("********************");
                var list = from s in studentList
                           where s.Age < 30
                           select new
                           {
                               IdName = s.Id + s.Name,
                               ClassName = s.ClassId == 2 ? "高级班" : "其他班"
                           };

                foreach (var item in list)
                {
                    Console.WriteLine("Name={0}  Age={1}", item.ClassName, item.IdName);
                }
            }
            {
                Console.WriteLine("********************");
                var list = studentList.Where<Student>(s => s.Age < 30)
                                     .Select(s => new
                                     {
                                         Id = s.Id,
                                         ClassId = s.ClassId,
                                         IdName = s.Id + s.Name,
                                         ClassName = s.ClassId == 2 ? "高级班" : "其他班"
                                     })
                                     .OrderBy(s => s.Id)
                                     .OrderByDescending(s => s.ClassId)
                                     .ThenBy(s => s.ClassId)//排序
                                     .Skip(2)//跳过  分页
                                     .Take(3)//获取数据
                                     ;
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.ClassName}  Age={item.IdName}");
                }
            }
            List<Class> classList = new List<Class>()
                {
                    new Class()
                    {
                        Id=1,
                        ClassName="初级班"
                    },
                    new Class()
                    {
                        Id=2,
                        ClassName="高级班"
                    },
                    new Class()
                    {
                        Id=3,
                        ClassName="微信小程序"
                    },
                };
            {
                var list = from s in studentList
                           join c in classList on s.ClassId equals c.Id
                           select new
                           {
                               Name = s.Name,
                               CalssName = c.ClassName
                           };
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name},CalssName={item.CalssName}");
                }
            }
            {
                var list = studentList.Join(classList, s => s.ClassId, c => c.Id, (s, c) => new
                {
                    Name = s.Name,
                    CalssName = c.ClassName
                });
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name},CalssName={item.CalssName}");
                }
            }
            {//左连接   右连接就反过来
                var list = from s in studentList
                           join c in classList on s.ClassId equals c.Id
                           into scList
                           from sc in scList.DefaultIfEmpty()//
                           select new
                           {
                               Name = s.Name,
                               CalssName = sc == null ? "无班级" : sc.ClassName//c变sc，为空则用
                           };
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name},CalssName={item.CalssName}");
                }
                Console.WriteLine(list.Count());
            }
            {
                var list = studentList.Join(classList, s => s.ClassId, c => c.Id, (s, c) => new
                {
                    Name = s.Name,
                    CalssName = c.ClassName
                }).DefaultIfEmpty();//为空就没有了
                foreach (var item in list)
                {
                    Console.WriteLine($"Name={item.Name},CalssName={item.CalssName}");
                }
                Console.WriteLine(list.Count());
            }
            #endregion
        }

    }
}
