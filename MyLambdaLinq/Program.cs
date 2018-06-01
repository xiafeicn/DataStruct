using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLambdaLinq.Extend;

namespace MyLambdaLinq
{
    /// <summary>
    /// 1 委托简介
    /// 2 匿名方法 匿名类 var
    /// 3 lambda表达式 goes to
    /// 4 系统自带委托Action/Func
    /// 5 扩展方法
    /// 6 linq扩展  
    /// 7 linq简单回顾
    /// 8 作业部署
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今天是Eleven老师为大家带来的lambda/linq课程");
                {
                    LambdaShow show = new LambdaShow();
                    show.Show();
                }

                #region 匿名类 var
                {
                    Student student = new Student()
                    {
                        Id = 123,
                        Name = "Smith"
                    };
                    student.Study();

                    //声明一个model  然后在实例化
                    object user = new
                    {
                        Id = 1,
                        Name = "Eleven",
                        Description = "这是老师",
                        Time = DateTime.Now
                    };
                    //user.Id;
                    //1 只能声明局部变量，不能是字段、也不能是静态属性
                    //2 声明的时候必须被初始化;

                    //var zzz;

                    //var zzz = null;

                    //dynamic  4.0 本质是个字典
                    //dynamic dynamicValue = 123;
                    //Console.WriteLine(dynamicValue.Id);

                    var i = 1;
                    Console.WriteLine(i);
                    //i = "";

                    //int i = 1;

                    var model = new
                    {
                        Id = 1,
                        Name = "Alpha Go",
                        Age = 21,
                        ClassId = 2
                    };

                    Console.WriteLine(model.Id);
                    Console.WriteLine(model.Name);
                    //model.Name = "张伟东";//只读
                    //复杂类型的编写

                    var name = "";
                    Console.WriteLine(name);

                    //var id;
                }
                #endregion

                #region 扩展方法
                {
                    int? iValue = null;
                    int? ivalue2 = 1;

                    ExtendShow.ToInt(iValue);
                    iValue.ToInt();

                    new LambdaShow().ExtendLambdaShow(123, "456");
                    new LambdaShow().Show();

                    //int.Parse("s");
                    iValue.DoNothing();
                    "s".DoNothing();
                    new LambdaShow().DoNothing();

                }
                #endregion

                #region linq
                {
                    LinqShow show = new LinqShow();
                    show.Show();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        public (string, string, int) Get()
        {
            return ("", "", 123);
        }
    }
}
