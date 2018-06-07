using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionDemo
{
    /// <summary>
    /// 1 表达式目录树
    /// 2 动态拼装表达目录树和扩展应用
    /// 3 解析表达式目录树，生成sql
    /// 4 表达式树的拼装链接
    /// 5 作业
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今晚学习表达式目录树Expression");
                //{
                //    Console.WriteLine("****************认识表达式目录树*************");
                //    ExpressionTest.Show();
                //}
                //{
                //    Console.WriteLine("********************MapperTest********************");
                //    ExpressionTest.MapperTest();
                //}
                {
                    Console.WriteLine("********************解析表达式目录树********************");
                    ExpressionVisitorTest.Show();
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
