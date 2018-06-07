using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionDemo.Extend;
using System.Diagnostics;
using ExpressionDemo.MappingExtend;

namespace ExpressionDemo
{
    /// <summary>
    /// 认识/拼装 表达式目录树
    /// 拼装表达式
    /// 应用
    /// </summary>
    public class ExpressionTest
    {
        public static void Show()
        {
            {
                //ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "m");
                //ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "n");
                //var binaryExpression = Expression.Add(Expression.Multiply(parameterExpression, parameterExpression2), Expression.Constant(2, typeof(int)));
                //Expression<Func<int, int, int>> expression = Expression.Lambda<Func<int, int, int>>(binaryExpression,
                //    new ParameterExpression[]
                //    {
                //        parameterExpression,
                //        parameterExpression2
                //    });
                //expression.Compile();
            }


            {
                Console.WriteLine("***************Lambda表达式******************");
                Func<int, int, int> func = (m, n) => m * n + 2;//Lambda表达式

                Console.WriteLine("***************Lambda表达式目录树******************");
                Expression<Func<int, int, int>> exp = (m, n) => m * n + 2;//lambda表达式声明 表达式目录树
                                                                          //Expression<Func<int, int, int>> exp1 = (m, n) =>
                                                                          //    {
                                                                          //        return m * n + 2;
                                                                          //    };

                Console.WriteLine("***************************************");
                //表达式目录树：语法树，或者说是一种数据结构
                int iResult1 = func.Invoke(12, 23);

                int iResult2 = exp.Compile().Invoke(12, 23);//Compile把表达式目录树编译成委托
            }

            //自己拼装表达式目录树
            {
                //常量
                ConstantExpression conLeft = Expression.Constant(345);
                ConstantExpression conRight = Expression.Constant(456);
                BinaryExpression binary = Expression.Add(conLeft, conRight);//345+456

                Expression<Func<int>> actExpression = Expression.Lambda<Func<int>>(binary, null);
                //只能执行表示Lambda表达式的表达式目录树，即LambdaExpression或者Expression<TDelegate>类型。如果表达式目录树不是表示Lambda表达式，需要调用Lambda方法创建一个新的表达式
                Func<int> func = actExpression.Compile();
                int iResult = func.Invoke();//()=>345+456
            }

            {
                // Expression<Func<int, int, int>> exp = (m, n) => m * n + 2;
                ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "m");//m
                ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "n");//n
                BinaryExpression binaryExpression = Expression.Multiply(parameterExpression, parameterExpression2);//m*n
                ConstantExpression constantExpression = Expression.Constant(2, typeof(int));//2
                BinaryExpression binaryExpressionAdd = Expression.Add(binaryExpression, constantExpression);//M*N+2  方法体
                Expression<Func<int, int, int>> exp = Expression.Lambda<Func<int, int, int>>(binaryExpressionAdd,
                    new ParameterExpression[]//参数列表
                {
                    parameterExpression,
                    parameterExpression2
                });

                int iResult2 = exp.Compile().Invoke(12, 23);
            }
            {
                // Expression<Func<int, int, int>> exp = (m, n) => m * (n + 2);
                ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "m");//m
                ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "n");//n
                ConstantExpression constantExpression = Expression.Constant(2, typeof(int));//2
                BinaryExpression binaryExpressionAdd = Expression.Add(parameterExpression2, constantExpression);//n+2
                BinaryExpression binaryExpressionMutiply = Expression.Multiply(parameterExpression, binaryExpressionAdd);//m * (n + 2)
                Expression<Func<int, int, int>> exp = Expression.Lambda<Func<int, int, int>>(binaryExpressionMutiply,
                    new ParameterExpression[]//参数列表
                {
                    parameterExpression,
                    parameterExpression2
                });

                int iResult2 = exp.Compile().Invoke(12, 23);
            }


            {
                ////以前根据用户输入拼装条件
                //string sql = "SELECT * FROM USER WHERE 1=1";

                //string name = Console.ReadLine();
                //if (string.IsNullOrWhiteSpace(name))//看有没有输入
                //{
                //    sql += $" and name like '%{name}%'";
                //}
                //if (string.IsNullOrWhiteSpace(name))//看有没有输入
                //{
                //    sql += $" and name like '%{name}%'";
                //}



                //现在entity framework查询的时候，需要一个表达式目录树
                IQueryable<int> list = (new List<int>()).AsQueryable();

                //if (true)//只过滤A
                //{
                //    Expression<Func<int, bool>> exp1 = x => x > 1;
                //}
                //if (true)//只过滤B
                //{
                //    Expression<Func<int, bool>> exp2 = x => x > 2;
                //}
                //if (true)//只过滤C
                //{
                //    Expression<Func<int, bool>> exp2 = x => x > 3;
                //}
                ////都过滤
                //Expression<Func<int, bool>> exp3 = x => x > 1 && x > 2&&x > 3;

                ////list.ToList();
                //有暴露全部数据的危险
                //if (true)//只过滤A
                //{
                //    //Expression<Func<int, bool>> exp1 = x => x > 1;
                //    list = list.Where(x => x > 1);
                //}
                //if (true)//只过滤B
                //{
                //    //Expression<Func<int, bool>> exp2 = x => x > 2;
                //    list = list.Where(x => x > 2);//延迟查询
                //}

                //list.Where()
                //拼装表达式目录树，交给下端用，而不是把集合暴露出来
                //Expression<Func<People, bool>> lambda = x => x.Age > 5;
                ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "x");

                Expression propertyExpression = Expression.Property(parameterExpression, typeof(People).GetProperty("Age"));
                //Expression property = Expression.Field(parameterExpression, typeof(People).GetField("Id"));
                ConstantExpression constantExpression = Expression.Constant(5, typeof(int));
                BinaryExpression binary = Expression.GreaterThan(propertyExpression, constantExpression);//添加方法的

                Expression<Func<People, bool>> lambda = Expression.Lambda<Func<People, bool>>(binary, new ParameterExpression[]
                {
                    parameterExpression
                });
                bool bResult = lambda.Compile()(new People()
                {
                    Id = 11,
                    Name = "Eleven",
                    Age = 31
                });
            }
            {
                //Expression<Func<People, bool>> lambda = x => x.Id.ToString().Equals("5");
                ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "x");
                Expression field = Expression.Field(parameterExpression, typeof(People).GetField("Id"));
                MethodCallExpression toString = Expression.Call(field, typeof(People).GetMethod("ToString"), new Expression[0]);
                ConstantExpression constantExpression = Expression.Constant("5", typeof(string));

                MethodCallExpression equals = Expression.Call(toString, typeof(People).GetMethod("Equals"), new Expression[] { constantExpression });
                Expression<Func<People, bool>> lambda = Expression.Lambda<Func<People, bool>>(equals, new ParameterExpression[]
                {
                    parameterExpression
                });
                bool bResult = lambda.Compile()(new People()
                {
                    Id = 11,
                    Name = "Eleven",
                    Age = 31
                });
            }


            {
                Console.WriteLine("****************************************");
                People people = new People()
                {
                    Id = 11,
                    Name = "Eleven",
                    Age = 31
                };
                PeopleCopy peopleCopy = new PeopleCopy()
                {
                    Id = people.Id,
                    Name = people.Name,
                    Age = people.Age
                };//硬编码：效率最高     Student--StudentCopy    Teacher--teacherCopy

                //Expression<Func<People, PeopleCopy>> lambda = p =>
                //        new PeopleCopy()
                //        {
                //            Id = p.Id,
                //            Name = p.Name,
                //            Age = p.Age
                //        };
                //lambda.Compile()(people);

                ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();
                foreach (var item in typeof(PeopleCopy).GetProperties())
                {
                    MemberExpression property = Expression.Property(parameterExpression, typeof(People).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                foreach (var item in typeof(PeopleCopy).GetFields())
                {
                    MemberExpression property = Expression.Field(parameterExpression, typeof(People).GetField(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(PeopleCopy)), memberBindingList.ToArray());
                Expression<Func<People, PeopleCopy>> lambda = Expression.Lambda<Func<People, PeopleCopy>>(memberInitExpression, new ParameterExpression[]
                {
                    parameterExpression
                });

                Func<People, PeopleCopy> func = lambda.Compile();
                PeopleCopy copy = func(people);

                //ExpressionMapper.Trans<People, PeopleCopy>(people);
                //ExpressionMapper.Trans<People, PeopleCopy>(people);

                //ExpressionGenericMapper<People, PeopleCopy>.Trans(people);
                //ExpressionGenericMapper<People, PeopleCopy>.Trans(people);
            }
        }

        public static void MapperTest()
        {
            People people = new People()
            {
                Id = 11,
                Name = "Eleven",
                Age = 31
            };

            long common = 0;
            long generic = 0;
            long cache = 0;
            long reflection = 0;
            long serialize = 0;
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    PeopleCopy peopleCopy = new PeopleCopy()
                    {
                        Id = people.Id,
                        Name = people.Name,
                        Age = people.Age
                    };
                }
                watch.Stop();
                common = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    PeopleCopy peopleCopy = ReflectionMapper.Trans<People, PeopleCopy>(people);
                }
                watch.Stop();
                reflection = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    PeopleCopy peopleCopy = SerializeMapper.Trans<People, PeopleCopy>(people);
                }
                watch.Stop();
                serialize = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    PeopleCopy peopleCopy = ExpressionMapper.Trans<People, PeopleCopy>(people);
                }
                watch.Stop();
                cache = watch.ElapsedMilliseconds;
            }
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    PeopleCopy peopleCopy = ExpressionGenericMapper<People, PeopleCopy>.Trans(people);
                }
                watch.Stop();
                generic = watch.ElapsedMilliseconds;
            }

            Console.WriteLine($"common = { common} ms");
            Console.WriteLine($"reflection = { reflection} ms");
            Console.WriteLine($"serialize = { serialize} ms");
            Console.WriteLine($"cache = { cache} ms");
            Console.WriteLine($"generic = { generic} ms");

        }


    }
}
