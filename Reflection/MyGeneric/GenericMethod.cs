using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class GenericMethod
    {
        /// <summary>
        /// 方法名字后面带上尖括号  类型参数
        /// .net framework2.0 CLR升级的
        /// 
        /// 延迟声明：声明方法的时候并没有指定参数类型，而是等到调用的时候指定
        /// 延迟思想：推迟一切可以推迟的
        /// 
        /// 编译的时候  类型参数编译为占位符
        /// 程序运行的时候，jit即时编译替换为真实类型
        /// 
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public static void Show<T>(T tParameter)
        {
            Console.WriteLine("This is {0},parameter={2},type={1}",
                typeof(GenericMethod).Name, tParameter.GetType().Name, tParameter);
            //tParameter.
            //Console.WriteLine(tParameter.Id);
            //Console.WriteLine(tParameter.Name);
        }


        /// <summary>
        /// 打印个object值
        /// 1 object是一切类型的父类
        /// 2 通过继承，子类拥有父类的一切属性和行为；任何父类出现的地方，都可以用子类来代替
        /// 
        /// 值类型参数，会装箱拆箱
        /// </summary>
        /// <param name="oParameter"></param>
        public static void ShowObject(object oParameter)
        {
            Console.WriteLine("This is {0},parameter={2},type={1}",
                typeof(CommonMethod), oParameter.GetType().Name, oParameter);
        }
    }
}
