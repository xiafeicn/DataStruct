using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute
{
    /// <summary>
    /// 特性：一个class，继承自Attribute类
    /// 特性一般用Attribute结尾，然后在使用的时候可以去掉Attribute结尾
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CustomAttribute : Attribute
    {
        public CustomAttribute()
        { }
        public CustomAttribute(int id)
        { }

        public string Description { get; set; }

        public string Remark = null;

        public void Show()
        {
            Console.WriteLine(this.Description);
        }
    }

    public class CustomAttributeChild : CustomAttribute
    {

    }

    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            this._TableName = tableName;
        }
        private string _TableName = null;

        public string TableName
        {
            get
            {
                return this._TableName;
            }

        }
    }
}
