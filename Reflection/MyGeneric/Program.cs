using MyGeneric.Extend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    /// <summary>
    /// 1 引入泛型:延迟声明
    /// 2 如何声明和使用泛型
    /// 3 泛型的好处和原理
    /// 4 泛型类、泛型方法、泛型接口、泛型委托
    /// 5 泛型约束
    /// 6 协变 逆变(选修)
    /// 7 泛型缓存(选修)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今天是Eleven老师给大家带来的泛型Generic");


                //List<int>
                //List<string>

                Console.WriteLine(typeof(List<>));
                Console.WriteLine(typeof(Dictionary<,>));

                int iValue = 123;
                string sValue = "456";
                DateTime dtValue = DateTime.Now;

                object oValue = new object();

                Console.WriteLine("**************************");
                CommonMethod.ShowInt(iValue);
                //CommonMethod.ShowInt(sValue);
                CommonMethod.ShowString(sValue);
                CommonMethod.ShowDateTime(dtValue);

                Console.WriteLine("**************************");
                CommonMethod.ShowObject(oValue);
                CommonMethod.ShowObject(iValue);
                CommonMethod.ShowObject(sValue);
                CommonMethod.ShowObject(dtValue);


                Console.WriteLine("**************************");

                GenericMethod.Show<int>(iValue);
                GenericMethod.Show(iValue);//不指定类型参数，编译器自动推算
                //GenericMethod.Show<string>(iValue);//类型参数和参数类型必须吻合
                GenericMethod.Show<string>(sValue);
                GenericMethod.Show<DateTime>(dtValue);
                GenericMethod.Show<object>(oValue);


                //Monitor.Show();

                //GenericClass<int,int,int>.Do(1,2,4);


                Console.WriteLine("**************************");
                People people = new People()
                {
                    Id = 409,
                    Name = "苗宝尔"
                };

                Chinese chinese = new Chinese()
                {
                    Id = 123,
                    Name = "仙择名",

                };
                Hubei hubei = new Hubei()
                {
                    Id = 333,
                    Name = "青青"
                };
                Japanese japanese = new Japanese()
                {
                    Id = 123,
                    Name = "苍老师"
                };

                GenericMethod.Show(people);
                GenericMethod.Show(chinese);
                GenericMethod.Show(hubei);

                Console.WriteLine("***************************");
                //Constraint.Show(people);
                Constraint.Show(chinese);
                Constraint.Show(hubei);

                Constraint.ShowBasic(people);
                Constraint.ShowBasic(chinese);
                Constraint.ShowBasic(hubei);

                //Constraint.Show(iValue);
                //Constraint.Show(japanese);//


                //Constraint.ShowInterface(people);
                Constraint.ShowInterface(chinese);
                Constraint.ShowInterface(hubei);

                Console.WriteLine("***************************");
                GenericCacheTest.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
