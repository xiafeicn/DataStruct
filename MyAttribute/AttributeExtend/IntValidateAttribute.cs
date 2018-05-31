using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute.AttributeExtend
{
    /// <summary>
    /// 验证int类型是否在取值范围内
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IntValidateAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        public IntValidateAttribute(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }

        public override bool Validate(object oValue)
        {
            return oValue != null && int.TryParse(oValue.ToString(), out int num) && num >= this._Min && num <= this._Max;
        }
    }


}
