using System.Linq;

namespace KolTool.Base
{
    public class LanguagePriorityHelper
    {
        /// <summary>
        /// Generate Multi-Language
        /// </summary>
        /// <param name="currentLanguage">currentLanguage</param>
        /// <param name="en">enField</param>
        /// <param name="cn">cnField</param>
        /// <param name="needBR">line feed</param>
        /// <returns></returns>
        public static string GenerateMultiLanguage(string currentLanguage, string en, string cn, params bool[] needBR)
        {
            if (string.IsNullOrWhiteSpace(en))
                return cn;
            if (needBR.Length == 0 || !needBR[0])
                return string.Format("{0} <small class='text-light'>{1}</small>", en, cn);
            return string.Format("{0}<br /><small class='text-light'>{1}</small>", en, cn);
        }

        public static string GeneratePriorityLanguage(string currentLanguage, string en, string cn)
        {
            if (!string.IsNullOrEmpty(en))
            {
                en = en.Replace(" , ", ", ");
            }
            if (currentLanguage != null && currentLanguage.Equals(Consts.Language.EN))
            {
                return string.IsNullOrWhiteSpace(en) ? cn : en;
            }
            return string.IsNullOrWhiteSpace(cn) ? en : cn;
        }

        public static string GeneratePriorityLanguage(string currentLanguage, string[] en, string[] cn,string type)
        {
            var enStr = string.Empty;
            var cnStr = string.Empty;
            if (en != null&&en.Count()>0)
            {
                enStr += string.Join("<br>", en);
            }
            if (cn != null&&cn.Count()>0)
            {
                cnStr +=string.Join("<br>", cn);
            }
            if (currentLanguage != null && currentLanguage.Equals(Consts.Language.EN))
            {
                if (!string.IsNullOrEmpty(enStr))
                    return type.Equals("Inclusion") ? "<span>Inclusion criteria:</span><div>" + enStr+"</div>" : "<span>Exclusion criteria:</span><div>" + enStr+ "</div>";
                if (!string.IsNullOrEmpty(cnStr))
                    return type.Equals("Inclusion") ? "<span>Inclusion criteria:</span><div>" + cnStr+"</div>" : "<span>Exclusion criteria:</span><div>" + cnStr+ "</div>";
                return string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(cnStr))
                    return type.Equals("Inclusion") ? "<span>纳入标准:</span><div>" + cnStr+"</div>" : "<span>排除标准:</span><div>" + cnStr+ "</div>";
                if (!string.IsNullOrEmpty(enStr))
                    return type.Equals("Inclusion") ? "<span>纳入标准:</span><div>" + enStr+"</div>" : "<span>排除标准:</span><div>" + enStr+ "</div>";
                return string.Empty;
            }
        }
    }
}
