using Ruanmou.DB.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    public class SimpleFactory
    {
        private static string IDBHelperConfig = ConfigurationManager.AppSettings["IDBHelperConfig"];
        private static string DllName = IDBHelperConfig.Split(',')[1];
        private static string TypeName = IDBHelperConfig.Split(',')[0];

        public static IDBHelper CreateInstance()
        {
            Assembly assembly = Assembly.Load(DllName);//获取当前路径下的dll，不要后缀
            Type dbHelperType = assembly.GetType(TypeName);//获取类型
            object oDBHelper = Activator.CreateInstance(dbHelperType);
            return oDBHelper as IDBHelper;//类型转换
        }
    }
}
