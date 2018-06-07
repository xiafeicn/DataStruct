using Ruanmou.NetCore.Interface;
using System;

namespace Ruanmou.NetCore.Servcie
{
    public class TestServiceB : ITestServiceB
    {
        public TestServiceB(ITestServiceA iTestService)
        { }

        public void Show()
        {
            Console.WriteLine("B123456");
        }
    }
}
