using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute.AttributeExtend
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    public class BaseDAL
    {
        /// <summary>
        /// 拼装sql、保存
        /// 增加校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Save<T>(T t)
        {
            Type type = t.GetType();
            string tableName = "";
            //Sql--表名称

            {
                object[] oAttributeArray = type.GetCustomAttributes(typeof(TableAttribute), true);//特性类的实例化就在反射发生的时候
                foreach (var oAttribute in oAttributeArray)
                {
                    TableAttribute tableAttribute = oAttribute as TableAttribute;
                    tableName = tableAttribute.TableName;
                    Console.WriteLine(tableName);
                }
            }

            bool isSafe = true;
            {
                foreach (var property in type.GetProperties())
                {
                    object[] oAttributeArray = property.GetCustomAttributes(typeof(AbstractValidateAttribute), true);//特性类的实例化就在反射发生的时候
                    foreach (var oAttribute in oAttributeArray)
                    {
                        AbstractValidateAttribute validateAttribute = oAttribute as AbstractValidateAttribute;
                        isSafe = validateAttribute.Validate(property.GetValue(t));
                        if (!isSafe)
                        {
                            break;
                        }
                    }
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }

            if (isSafe)
                Console.WriteLine("保存到数据库");
            else
                Console.WriteLine("数据不合法");












            //{
            //    object[] oAttributeArray = type.GetCustomAttributes(typeof(CustomAttribute), true);//特性类的实例化就在反射发生的时候
            //    foreach (var oAttribute in oAttributeArray)
            //    {
            //        CustomAttribute customAttribute = oAttribute as CustomAttribute;
            //        Console.WriteLine(customAttribute?.Remark);

            //        //如果有这特性  就打印个日志
            //        //如果有这特性  。。。
            //        //如果有这特性  。。。。
            //        Console.WriteLine();
            //    }
            //}
            //{
            //    PropertyInfo property = type.GetProperty("Id");
            //    object[] oAttributeArray = property.GetCustomAttributes(typeof(CustomAttribute), true);
            //    foreach (var oAttribute in oAttributeArray)
            //    {
            //        CustomAttribute customAttribute = oAttribute as CustomAttribute;
            //        Console.WriteLine(customAttribute?.Remark);
            //    }
            //}











        }
    }
}
