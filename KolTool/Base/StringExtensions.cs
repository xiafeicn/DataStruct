using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace KolTool.Base
{
    public static class StringExtensions
    {
        internal const string TRUE = "TRUE";
        internal const string FALSE = "FALSE";
        internal const char TRIM_CHAR = '\\';
        internal const string SLASH = "/";
        internal const string BACKSLASH = "\\";


        public static bool ContainsKey(this Dictionary<string, string> dic, string key)
        {
            return dic.Keys.Contains(key, StringComparer.OrdinalIgnoreCase);
            //if (list == null || string.IsNullOrWhiteSpace(key)) return false;
            //return list.Any(t => t.Key != null && t.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        } /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetMonday(this DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周第二天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetTuesday(this DateTime datetime)
        {
            return datetime.GetMonday().AddDays(1);
        }
        public static string GetValue(this Dictionary<string, string> dic, string key)
        {
            return dic[key];
            //if (list == null || string.IsNullOrWhiteSpace(key)) return string.Empty;
            //if (list.Any(t => t.Key.Equals(key, StringComparison.OrdinalIgnoreCase)))
            //{
            //    return list.FirstOrDefault(t => t.Key != null && t.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;
            //}

            //return string.Empty;
        }

        public static string[] NullToStringArry(this object obj)
        {

            if (obj == null)
            {
                return new string[] { };
            }
            else
            {
                return obj as string[];
            }
        }
        public static bool ToBoolean(this string trueOrFalse)
        {
            var ret = false;
            if (string.IsNullOrEmpty(trueOrFalse) == false)
            {
                ret = trueOrFalse.Trim().EqualsOrdinalIgnoreCase(TRUE);
            }

            return ret;
        }

        public static double ToDouble(this string doubleString)
        {
            double dValue = 0.0;
            double.TryParse(doubleString, out dValue);
            return dValue;
        }

        public static bool EqualsOrdinalIgnoreCase(this string A, string B)
        {
            return string.Equals(A, B, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToLongDateTimeFormat(this String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToMiddleDateTimeFormat(this String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(str, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        public static DateTime? ToDateTime(this string obj)
        {
            try
            {
                return DateTime.Parse(obj);
            }
            catch
            {
                return null;
            }
        }
        public static long UnixTimestampFromDateTime(this DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerMillisecond;
            return unixTimestamp;
        }

        public static string ToDateTimeString(this string obj)
        {
            var dt = obj.ToDateTime();

            if (dt.HasValue)
            {
                return dt.Value.ToString("yyyy-MM-dd");
            }
            return String.Empty;
        }


        public static DateTime? ToFormalDateTime(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                DateTime time = new DateTime();
                if (DateTime.TryParse(obj, null, DateTimeStyles.None, out time))
                    return time;
                return null;
            }
            return null;
        }

  

        public static String ToMinuteSecond(this String str)
        {
            var timeString = string.Empty;

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                var timeSpan = new TimeSpan(0, 0, int.Parse(str));

                if (timeSpan.Hours < 1) timeString = timeSpan.ToString().Remove(0, 3);
                else timeString = timeSpan.ToString();
            }

            return timeString;
        }

        public static int ToInt(this string strValue)
        {
            int iValue = 0;
            int.TryParse(strValue, out iValue);
            return iValue;
        }

        public static Nullable<int> ToNullableInt(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return null;
            return obj.ToInt();
        }

        public static bool ToBool(this string obj)
        {
            var ret = false;
            bool.TryParse(obj, out ret);
            return ret;
        }

        public static Nullable<bool> ToNullableBool(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return null;
            return obj.ToBool();
        }

        public static float ToFloat(this string obj)
        {
            if (string.IsNullOrEmpty(obj)) return 0.0f;
            else return Convert.ToSingle(obj);
        }

        public static Nullable<float> ToNullableFloat(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return null;
            return obj.ToFloat();
        }

        public static decimal ToDecimal(this string obj)
        {
            if (string.IsNullOrEmpty(obj)) return 0;
            else return Decimal.Parse(obj);
        }

        public static Nullable<decimal> ToNullableDecimal(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return null;
            return obj.ToDecimal();
        }

        public static long ToLong(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return 0;
            return long.Parse(obj);
        }

        public static Nullable<long> ToNullableLong(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return null;
            return obj.ToLong();
        }

        public static string CombineCurrentAppDomainPath(this String path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        public static string ReplaceWhiteSpacesWithOneSpace(this string strValue)
        {
            Regex r = new Regex(@"\s+");

            return r.Replace(strValue, " ");
        }

        public static string SubStringBySDBC(this string strData, int startindex, int length, int codepage = 0)
        {
            string strRtn = string.Empty;
            byte[] arybytData = Encoding.GetEncoding(codepage).GetBytes(strData);
            byte[] arybytTemp = new byte[length];
            Array.Copy(arybytData, startindex, arybytTemp, 0, length);
            strRtn = Encoding.GetEncoding(codepage).GetString(arybytTemp, 0, length);
            if (Encoding.GetEncoding(codepage).GetByteCount(strRtn) > length)
            {
                strRtn = strRtn.Substring(0, strRtn.Length - 1); ;
            }
            return strRtn;
        }

        public static int GetLengthByBytes(this string strValue, int codepage = 0)
        {
            int iRtn = 0;
            if (string.IsNullOrEmpty(strValue) == false)
                iRtn = Encoding.GetEncoding(codepage).GetByteCount(strValue);
            return iRtn;
        }

        public static string NullToString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        public static int NullToInt(this object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        public static bool IsArrayEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str) || str.Equals("[]");
        }

        public static Nullable<DateTime> StringToDateTime(this string dateTime)
        {
            if (string.IsNullOrWhiteSpace(dateTime))
            {
                return null;
            }
            if (Convert.ToDateTime(dateTime) == new DateTime(1900, 1, 1))
            {
                return null;
            }
            return Convert.ToDateTime(dateTime);
        }

        public static bool IsInt(this object obj)
        {
            try
            {
                int.Parse(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ParserUrl(this Uri uri)
        {
            var host = uri.OriginalString;
            if (!string.IsNullOrWhiteSpace(uri.PathAndQuery))
            {
                host = uri.PathAndQuery == "/" ? host.TrimEnd('/') : host.Replace(uri.PathAndQuery, "");
            }
            host = host.Replace(":" + uri.Port, "");
            return string.Concat(host, uri.PathAndQuery);
        }



        #region alias转义
        public static String EscapeQueryChars(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                // These characters are part of the query syntax and must be escaped
                if (c == '\\' || c == '+' || c == '-' || c == '!' || c == '(' || c == ')' || c == ':'
                  || c == '^' || c == '[' || c == ']' || c == '\"' || c == '{' || c == '}' || c == '~'
                  || c == '*' || c == '?' || c == '|' || c == '&' || c == ';' || c == '/'
                  || string.IsNullOrWhiteSpace(c.ToString()))
                {
                    sb.Append('\\');
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static String ReplaceSpecialChars(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                if (c != '\\' && c != '+' && c != '-' && c != '!' && c != '(' && c != ')' && c != ':'
                  || c != '^' && c != '[' && c != ']' && c != '\"' && c != '{' && c != '}' && c != '~'
                  || c != '*' && c != '?' && c != '|' && c != '&' && c != ';' && c != '/'
                  || string.IsNullOrWhiteSpace(c.ToString()))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string CharAt(this string s, int index)
        {
            if ((index >= s.Length) || (index < 0))
                return "";
            return s.Substring(index, 1);
        }
        #endregion

        public static string GenerateOrderNo()
        {
            Random ran = new Random();
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

    }
}
