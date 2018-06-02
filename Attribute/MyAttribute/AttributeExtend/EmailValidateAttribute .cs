using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidateAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            if (oValue != null && string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                return true;//正则验证下
            }
            else
            {
                return false;
            }
        }
    }


}
