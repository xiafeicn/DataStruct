using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLambdaLinq
{
    public delegate void NoReturnNoParaOutClass();
    public sealed class LambdaShow
    {
        public delegate void NoReturnNoPara();//1 委托的声明   委托是一种类型
        public delegate int WithReturnNoPara();
        public delegate void NoReturnWithPara(int id, string name);
        public delegate LambdaShow WithReturnWithPara(DateTime time);
        public delegate LambdaShow WithReturnWithParaRefOut(ref DateTime time, out int i);

        public void Show()
        {
            Student student = new Student()
            {
                Id = 123,
                Name = "Smith"
            };
            student.Study();

            {
                NoReturnNoPara method = new NoReturnNoPara(this.DoNothing1);//2委托的实例化
                method.Invoke();//3 委托实例的调用
                method();
                this.DoNothing1();
            }
            {
                NoReturnWithPara method = new NoReturnWithPara(this.DoNothing);
                //NoReturnWithPara method = this.DoNothing;
                method.Invoke(123, "珍惜");
            }
            {
                NoReturnWithPara method = new NoReturnWithPara(
                    delegate (int id, string name)//匿名方法
                    {
                        Console.WriteLine($"{id} {name} DoNothing out");
                    }
                    );
                method.Invoke(123, "绚烂的夏");
            }
            {
                NoReturnWithPara method = new NoReturnWithPara(
                     (int id, string name) =>//goes to  lambda表达式：匿名方法--方法
                    {
                        Console.WriteLine($"{id} {name} DoNothing out");
                    }
                    );
                method.Invoke(123, "追梦");
            }
            {
                NoReturnWithPara method = new NoReturnWithPara(
                     (id, name) =>//委托约束，去掉参数类型，自动推算
                     {
                         Console.WriteLine($"{id} {name} DoNothing out");
                     }
                    );
                method.Invoke(123, "装逼的岁月");
            }
            {
                NoReturnWithPara method = new NoReturnWithPara(
                     (id, name) => Console.WriteLine($"{id} {name} DoNothing out")
                     //方法体只有一行，就可以把大括号和分号去掉
                    );
                method.Invoke(123, "美羡");
            }
            {
                NoReturnWithPara method = (id, name) => Console.WriteLine($"{id} {name} DoNothing out");
                //委托实例化的时候可以不要new NoReturnWithPara
                method.Invoke(123, "晴月");
            }

            {
                //0到16个参数的无返回值泛型委托
                Action act1 = () => { };
                Action<string> act2 = t => Console.WriteLine("1234");
                Action<string, int, DateTime, long, decimal, string, int, DateTime, long, decimal, string, int, DateTime, long, decimal, decimal> act3 = null;
            }
            {
                //0到16个参数的带一个返回值泛型委托
                Func<string> func1 = () => DateTime.Now.ToShortDateString();
                Console.WriteLine(func1.Invoke());
                Func<string, int> func2 = t => DateTime.Now.Millisecond;
                Console.WriteLine(func2.Invoke("123"));
                Func<string, int, DateTime, long, decimal, string, int, DateTime, long, decimal, string, int, DateTime, long, decimal, decimal, int> func3 = null;


                //多个结果：返还对象/ref  out/tuple
            }


        }

        private void DoNothing(int id, string name)
        {
            Console.WriteLine("{0} {1} DoNothing out");
        }

        private void DoNothing1()
        {
            Console.WriteLine("DoNothing1");
        }

        private void DoNothing2()
        {
            Console.WriteLine("DoNothing2");
        }
    }


    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17>
        (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17);

}
