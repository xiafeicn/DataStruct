using Ruanmou.DB.Interface;
using Ruanmou.DB.MySql;
using Ruanmou.DB.SqlServer;
using Ruanmou.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    /// <summary>
    /// 1 dll-IL-metadata-反射
    /// 2 反射加载dll，读取module、类、方法、特性
    /// 3 反射创建对象，反射+简单工厂+配置文件  选修:破坏单例 创建泛型
    /// 4 反射调用实例方法、静态方法、重载方法  选修:调用私有方法 调用泛型方法
    /// 5 反射字段和属性，分别获取值和设置值
    /// 6 反射的好处和局限
    /// 
    /// 反射：是.net framework提供一个访问metadat的帮助类库，可以获取信息并使用
    /// 
    /// 好处：
    /// 动态
    /// 缺点：
    /// 1 很麻烦
    /// 2 避开编译器检查
    /// 3 性能问题: 
    ///    耗时绝对值小，一般不会影响到程序性能
    ///    最多相差不到300倍，优化后不到10倍
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //#region dll 破解

                // Assembly pojie = Assembly.Load(@"C:\Users\fei.xia\Downloads\8.Oraycn.Demos.VideoChat\dlls\OMCS.dll");//获取当前路径下的dll，不要后缀
                //foreach (var item in pojie.GetModules())
                //{
                //    Console.WriteLine(item.Name);
                //}
                //foreach (var item in pojie.GetTypes())
                //{
                //    Console.WriteLine(item.Name);
                //}
                //#endregion

                Console.WriteLine("欢迎来到.net高级班vip课程，今天是Eleven老师为大家带来的反射的课程");
                #region Common
                {
                    Console.WriteLine("************************Common*****************");
                    IDBHelper iDBHelper = new SqlServerHelper();//new MySqlHelper();
                    iDBHelper.Query();
                }
                #endregion

                #region Common
                {
                    Console.WriteLine("************************Reflection*****************");
                    Assembly assembly = Assembly.Load("Ruanmou.DB.MySql");//获取当前路径下的dll，不要后缀
                    //Assembly assembly1 = Assembly.LoadFile(@"D:\ruanmou\online9\20170607Advanced9Course2Reflection\MyReflection\MyReflection\bin\Debug\Ruanmou.DB.MySql.dll");
                    Assembly assembly2 = Assembly.LoadFrom("Ruanmou.DB.MySql.dll");
                    foreach (var item in assembly.GetModules())
                    {
                        Console.WriteLine(item.Name);
                    }
                    foreach (var item in assembly.GetTypes())
                    {
                        Console.WriteLine(item.Name);
                    }
                    //foreach (var item in assembly.GetCustomAttributes())
                    //{
                    //    Console.WriteLine(item.ToString());
                    //}
                    Type dbHelperType = assembly.GetType("Ruanmou.DB.MySql.MySqlHelper");//获取类型
                    object oDBHelper = Activator.CreateInstance(dbHelperType);//创建对象
                                                                              //oDBHelper.Query();
                                                                              //MySqlHelper iDBHelper = (MySqlHelper)oDBHelper;//类型转换
                    IDBHelper iDBHelper = oDBHelper as IDBHelper;//类型转换
                    iDBHelper.Query();
                }
                #endregion

                #region 反射+简单工厂+配置文件
                {
                    Console.WriteLine("************************反射+简单工厂+配置文件*****************");
                    IDBHelper iDBHelper = SimpleFactory.CreateInstance();
                    iDBHelper.Query();
                }
                #endregion

                #region 多构造函数 破坏单例 创建泛型 
                {
                    Console.WriteLine("************************多构造函数 破坏单例 创建泛型 *****************");
                    Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                    Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest");
                    foreach (var item in type.GetConstructors())
                    {
                        Console.WriteLine(item.Name);
                    }
                    object oTest = Activator.CreateInstance(type);
                    object oTest1 = Activator.CreateInstance(type, new object[] { 784 });
                    object oTest2 = Activator.CreateInstance(type, new object[] { "lz" });

                    Type singletonType = assembly.GetType("Ruanmou.DB.SqlServer.Singleton");
                    Singleton singleton = Singleton.GetInstance();// new Singleton();
                    object oSingleton1 = Activator.CreateInstance(singletonType, true);
                    object oSingleton2 = Activator.CreateInstance(singletonType, true);
                    object oSingleton3 = Activator.CreateInstance(singletonType, true);
                    object oSingleton4 = Activator.CreateInstance(singletonType, true);

                    Type genericType = assembly.GetType("Ruanmou.DB.SqlServer.GenericClass`3");
                    Type typeNew = genericType.MakeGenericType(typeof(int), typeof(int), typeof(int));
                    object oGeneric = Activator.CreateInstance(typeNew);
                }
                #endregion

                #region 反射调用实例方法、静态方法、重载方法 
                {
                    Console.WriteLine("************************反射调用实例方法、静态方法、重载方法*****************");
                    Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                    Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);
                    foreach (var item in type.GetMethods())
                    {
                        Console.WriteLine(item.Name);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show1");
                        method.Invoke(oTest, null);
                    }
                    {
                        MethodInfo method = type.GetMethod("Show5");
                        method.Invoke(oTest, new object[] { "电脑信息技术" });
                        method.Invoke(null, new object[] { "装逼的岁月" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int) });
                        method.Invoke(oTest, new object[] { 123 });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string) });
                        method.Invoke(oTest, new object[] { "吝啬小兔" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                        method.Invoke(oTest, new object[] { 123, "浅步调" });
                    }
                    {
                        MethodInfo method = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                        method.Invoke(oTest, new object[] { "心里要有阳光", 123 });
                    }
                }
                #endregion

                #region 调用私有方法 调用泛型方法
                {
                    Console.WriteLine("************************调用私有方法 调用泛型方法*****************");
                    Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");
                    Type type = assembly.GetType("Ruanmou.DB.SqlServer.ReflectionTest");
                    object oTest = Activator.CreateInstance(type);

                    {
                        MethodInfo method = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
                        method.Invoke(oTest, new object[] { "幸福你我" });
                    }

                    Type genericType = assembly.GetType("Ruanmou.DB.SqlServer.GenericMethod");
                    object oGeneric = Activator.CreateInstance(genericType);
                    {
                        MethodInfo method = genericType.GetMethod("Show");
                        MethodInfo methodNew = method.MakeGenericMethod(typeof(int), typeof(int), typeof(int));
                        methodNew.Invoke(oGeneric, new object[] { 1, 2, 3 });
                    }
                }
                #endregion

                #region 字段和属性
                {
                    Console.WriteLine("************************Common*****************");
                    People people = new People();
                    people.Id = 506;
                    people.Name = "yoyo";
                    people.Description = "勤学的聪明妹子";
                    Console.WriteLine("Id={0}", people.Id);
                    Console.WriteLine("Name={0}", people.Name);
                    Console.WriteLine("Description={0}", people.Description);

                    Console.WriteLine("************************字段和属性*****************");
                    Type type = typeof(People);
                    object oPeople = Activator.CreateInstance(type);
                    foreach (var item in type.GetProperties())
                    {
                        Console.WriteLine("{0}={1}", item.Name, item.GetValue(oPeople));
                        if (item.Name.Equals("Id"))
                        {
                            item.SetValue(oPeople, 787);
                        }
                        else if (item.Name.Equals("Name"))
                        {
                            item.SetValue(oPeople, "我行我素");
                        }
                        Console.WriteLine("{0}={1}", item.Name, item.GetValue(oPeople));
                    }
                    foreach (var item in type.GetFields())
                    {
                        Console.WriteLine("{0}={1}", item.Name, item.GetValue(oPeople));
                        item.SetValue(oPeople, "聪明的小伙子");
                        Console.WriteLine("{0}={1}", item.Name, item.GetValue(oPeople));
                    }
                }

                #endregion

                #region sql
                {
                    SqlServerHelper helper = new SqlServerHelper();
                    Company company = helper.Get<Company>(1);
                }
                #endregion

                #region Monitor
                {
                    Monitor.Show();
                }
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
