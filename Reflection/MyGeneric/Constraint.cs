using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class Constraint
    {
        /// <summary>
        /// 有约束才有自由  有权利就得有义务
        /// 
        /// 1 基类约束，就可以访问基类的属性和方法
        /// 2 必须是基类/子类
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public static void Show<T>(T tParameter) where T : People, ISports, IWork, new()   //基类约束  而且
        {
            Console.WriteLine("This is {0},parameter={2},type={1}",
                typeof(GenericMethod).Name, tParameter.GetType().Name, tParameter);
            //tParameter.
            Console.WriteLine(tParameter.Id);
            Console.WriteLine(tParameter.Name);
            tParameter.Hi();
        }

        public static void ShowInterface<T>(T tParameter) where T : ISports, IWork
        {
            Console.WriteLine("This is {0},parameter={2},type={1}",
                typeof(GenericMethod).Name, tParameter.GetType().Name, tParameter);
            tParameter.Pingpang();
        }

        /// <summary>
        /// 基类参数
        /// </summary>
        /// <param name="tParameter"></param>
        public static void ShowBasic(People tParameter)
        {
            Console.WriteLine("This is {0},parameter={2},type={1}",
                typeof(GenericMethod).Name, tParameter.GetType().Name, tParameter);
            //tParameter.
            Console.WriteLine(tParameter.Id);
            Console.WriteLine(tParameter.Name);
            tParameter.Hi();
        }

        public static T Get<T>()
            //where T : class//引用类型约束
            //where T : struct//值类型约束
            where T : new()//无参数构造函数约束
        {
            T t = new T();
            //return null;
            //return 0;
            return default(T);
        }



    }
}
