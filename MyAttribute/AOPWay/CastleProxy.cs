
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.DynamicProxy;//Castle.Core

namespace MyAttribute.AOPWay
{
    /// <summary>
    /// 使用Castle\DynamicProxy 实现动态代理
    /// </summary>
    public class CastleProxy
    {
        public static void Show()
        {
            User user = new User() { Name = "Eleven", Password = "123456" };
            ProxyGenerator generator = new ProxyGenerator();
            MyInterceptor interceptor = new MyInterceptor();
            UserProcessor userprocessor = generator.CreateClassProxy<UserProcessor>(interceptor);
            userprocessor.RegUser(user);
        }
        public interface IUserProcessor
        {
            void RegUser(User user);
        }

        public class UserProcessor : IUserProcessor
        {
            /// <summary>
            /// 必须带上virtual
            /// </summary>
            /// <param name="user"></param>
            public virtual void RegUser(User user)
            {
                Console.WriteLine("用户已注册。Name:{0},PassWord:{1}", user.Name, user.Password);
            }
        }

        public class MyInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                PreProceed(invocation);
                invocation.Proceed();
                PostProceed(invocation);
            }
            public void PreProceed(IInvocation invocation)
            {
                Console.WriteLine("方法执行前");
            }

            public void PostProceed(IInvocation invocation)
            {
                Console.WriteLine("方法执行后");
            }
        }
    }
}
