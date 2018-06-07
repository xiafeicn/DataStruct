using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Core.Project
{
    /// <summary>
    /// 1 framework/clr/C# ，C#6 C#7新语法，.net framework core
    /// 2 .net core MVC，Startup、controller、action、view、传值、session
    /// 3 各种filter:异常、权限、资源、Action、Result
    /// 
    /// 1 新管道模型：中间件Middleware 三种注册和实现
    /// 2 自带依赖注入和定制第三方依赖注入容器
    /// 3 EntityFrameworkCore 使用和封装
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今天是Eleven老师为大家带来的.Net Core");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
