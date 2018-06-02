using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric.Extend
{
    /// <summary>
    /// 只能放在接口或者委托的泛型参数前面
    /// out 协变covariant    修饰返回值 
    /// in  逆变contravariant  修饰传入参数
    /// </summary>
    public class CCTest
    {
        public static void Show()
        {
            //IEnumerable<int>
            //Action<int> act = null;


            {
                Bird bird1 = new Bird();
                Bird bird2 = new Sparrow();
                Sparrow sparrow1 = new Sparrow();
                //Sparrow sparrow2 = new Bird();
            }


            {
                //List<int> intList = new List<int>();
                List<Bird> birdList1 = new List<Bird>();

                //List<Bird> birdList2 = new List<Sparrow>();//不是父子关系，没有继承关系
                //一群麻雀一定是一群鸟

                List<Bird> birdList3 = new List<Sparrow>().Select(c => (Bird)c).ToList();
            }



            {//协变
                IEnumerable<Bird> birdList1 = new List<Bird>();

                IEnumerable<Bird> birdList2 = new List<Sparrow>();//协变
                //一群麻雀一定是一群鸟

                Func<Bird> func = new Func<Sparrow>(() => null);

                ICustomerListOut<Bird> customerList1 = new CustomerListOut<Bird>();
                ICustomerListOut<Bird> customerList2 = new CustomerListOut<Sparrow>();
            }



            {//逆变
                ICustomerListIn<Sparrow> customerList2 = new CustomerListIn<Sparrow>();
                ICustomerListIn<Sparrow> customerList1 = new CustomerListIn<Bird>();

                ICustomerListIn<Bird> birdList1 = new CustomerListIn<Bird>();
                birdList1.Show(new Sparrow());
                birdList1.Show(new Bird());

                Action<Sparrow> act = new Action<Bird>((Bird i) => { });
            }


            {
                IMyList<Sparrow, Bird> myList1 = new MyList<Sparrow, Bird>();
                IMyList<Sparrow, Bird> myList2 = new MyList<Sparrow, Sparrow>();//协变
                IMyList<Sparrow, Bird> myList3 = new MyList<Bird, Bird>();//逆变
                IMyList<Sparrow, Bird> myList4 = new MyList<Bird, Sparrow>();//协变+逆变

                //Func<string,long>
            }
        }
    }

    public class Bird
    {
        public int Id { get; set; }
    }
    /// <summary>
    /// 麻雀
    /// </summary>
    public class Sparrow : Bird
    {
        public string Name { get; set; }
    }





    /// <summary>
    /// 逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListIn<in T>
    {
        //T Get();//不能作为返回值

        void Show(T t);
    }

    public class CustomerListIn<T> : ICustomerListIn<T>
    {
        //public T Get()
        //{
        //    return default(T);
        //}

        public void Show(T t)
        {
        }
    }

    /// <summary>
    /// out 协变 只能是返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListOut<out T>
    {
        T Get();

        //void Show(T t);//T不能作为传入参数
    }

    /// <summary>
    /// 类没有协变逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomerListOut<T> : ICustomerListOut<T>
    {
        public T Get()
        {
            return default(T);
        }

        //public void Show(T t)
        //{

        //}
    }





    public interface IMyList<in inT, out outT>
    {
        void Show(inT t);
        outT Get();
        outT Do(inT t);

        ////out 只能是返回值   in只能是参数
        //void Show1(outT t);
        //inT Get1();

    }

    public class MyList<T1, T2> : IMyList<T1, T2>
    {

        public void Show(T1 t)
        {
            Console.WriteLine(t.GetType().Name);
        }

        public T2 Get()
        {
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }

        public T2 Do(T1 t)
        {
            Console.WriteLine(t.GetType().Name);
            Console.WriteLine(typeof(T2).Name);
            return default(T2);
        }
    }

}
