using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class GenericClass<W, Eleven, nothing>
    {
        public static void Do(W w, Eleven e, nothing n)
        { }
        public void Show(W w)
        { }

        public Eleven Get()
        {
            return default(Eleven);
        }
    }


    public interface IStudy<T>
    {
        T Study(T t);
    }

    public delegate int GetHandler();

    public delegate EveryThing GetHandler<EveryThing>();

    /// <summary>
    /// 普通类
    /// </summary>
    public class Child
        //: GenericClass<W, Eleven, nothing> //普通类不能直接继承泛型类
        //: GenericClass<int, string, double> //指定类型参数后才可以
        //: IStudy<T>//普通类不能直接实现泛型接口
        : IStudy<string>


    {
        public string Study(string t)
        {
            throw new NotImplementedException();
        }
    }

    public class GenericChild<Eleven, W>//等于声明了两个局部类型  W和Eleven
                                        //: GenericClass<W, Eleven, string> //泛型类可以直接继承泛型类
        : IStudy<Eleven>//泛型类可以直接实现泛型接口
    {
        //public string Study(string t)
        //{
        //    throw new NotImplementedException();
        //}

        Eleven IStudy<Eleven>.Study(Eleven t)
        {
            throw new NotImplementedException();
        }
    }

}
