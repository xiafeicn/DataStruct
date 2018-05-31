using MyAttribute.AOPWay;
using MyAttribute.AttributeExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute
{
    /// <summary>
    /// 1 特性attribute，和注释有什么区别
    /// 2 声明和使用attribute，AttributeUsage:
    /// 3 应用attribute
    /// 4 AOP面向切面
    /// 5 多种方式实现AOP
    /// 
    /// 注释是IDE环境使用的
    /// 特性：可以影响编译器/程序运行的行为
    /// 
    /// 特性：可以在不破坏类型封装的前提下，为对象增加额外的信息，执行额外的行为
    /// 
    /// 把公共逻辑移出去，只完成私有逻辑，
    /// 怎么实现呢？   我们可以写个类只完成私有逻辑，然后通过特性执行公共逻辑
    /// 
    /// OOP：面向对象
    /// AOP：面向切面编程
    /// 二者是互补的
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今天的内容是特性和AOP");
                {
                    People people = new People();
                }

                {
                    UserModel user = new UserModel();
                    user.Id = 1;
                    user.CompanyId = 999;
                    BaseDAL.Save<UserModel>(user);
                    string remark = UserState.Normal.GetRemark();
                }

                {
                    UserModel user = new UserModel();
                    user.Id = 1;
                    user.CompanyId = 9999;
                    BaseDAL.Save<UserModel>(user);
                    string remark = UserState.Normal.GetRemark();
                }


                #region AOP show
                Console.WriteLine("***********************");
                Decorator.Show();
                Console.WriteLine("***********************");
                Proxy.Show();
                Console.WriteLine("***********************");
                CastleProxy.Show();
                Console.WriteLine("***********************");
                UnityAOP.Show();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
