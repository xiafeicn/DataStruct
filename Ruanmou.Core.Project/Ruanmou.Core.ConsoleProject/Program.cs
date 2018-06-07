using System;
using System.Text;

namespace Ruanmou.Core.ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                Console.WriteLine("欢迎来到.net高级班vip课程，今天是Eleven老师为大家带来的.Net Core");
                Console.WriteLine("Hello World!");

                var user = new
                {
                    Id = 11,
                    Name = "Eleven"
                };
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(user));

                Console.WriteLine("**************************************");
                {
                    SharpSix six = new SharpSix();
                    People people = new People()
                    {
                        Id = 505,
                        Name = "马尔凯蒂"
                    };
                    six.Show(people);
                }

                Console.WriteLine("**************************************");
                {
                    SharpSeven seven = new SharpSeven();
                    seven.Show();
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