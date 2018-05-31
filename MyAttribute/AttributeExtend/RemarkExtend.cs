using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute.AttributeExtend
{
    /// <summary>
    /// 用于枚举增加描述的
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            _Remark = remark;
        }

        private string _Remark;

        public string Remark
        {
            get
            {
                return _Remark;
            }
        }
    }

    /// <summary>
    /// 枚举特性的扩展
    /// </summary>
    public static class RemarkExtend
    {
        public static string GetRemark(this Enum eValue)
        {
            Type type = eValue.GetType();
            FieldInfo field = type.GetField(eValue.ToString());
            RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
            return remarkAttribute.Remark;
        }
    }
}
