using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBI.Metrix.Model;
using GBI.Metrix.Model.Entity;
using GBI.Metrix.Service.Misc;
using GBI.Metrix.Utility;
using KolTool.Base;
using KolTool.Model;
using Newtonsoft.Json;

namespace GBI.Metrix.Service.Extension
{
    public static class JanusBaseEntityExtension
    {
        public static string GetName<T>(this T t, string language) where T : IJanusBaseEntity
        {
            if (t == null) return "";
            return LanguagePriorityHelper.GeneratePriorityLanguage(language, t.name_en, t.name_cn);
        }

        public static string GetEnCnName(this ENCN entity, string language)
        {
            string result = string.Empty;
            if (entity == null) return "";
            result = LanguagePriorityHelper.GeneratePriorityLanguage(language, entity.en, entity.cn) ?? "";
            result = result.TrimStart('[').TrimEnd(']').TrimStart('\"').TrimEnd('\"');
            return result;
        }

        public static string GetEntityName(this string id, string language, DataSetEnum module)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entities = HttpService.GetById<BaseEntity<IJanusBaseEntity>>(id, module);
                if (entities != null)
                {
                    var entity = entities.data.FirstOrDefault();
                    if (entity != null)
                    {
                        return entity.GetName(language);
                    }
                }
            }
            return string.Empty;
        }

        public static string GetArrayName(this ENCN[] list, string language)
        {
            if (list == null || list.Count() <= 0)
                return "";
            string result = string.Empty;
            var arrList = new List<string>();
            foreach (var item in list)
            {
                arrList.Add(item.GetEnCnName(language));
            }
            return string.Join("&nbsp", arrList);
        }


        public static string[] GetArrInfoName(this ENCNINFO[] list, string language)
        {
            if (list == null || list.Count() <= 0)
                return null;
            var arr = new string[2];
            var arr0 = new StringBuilder();
            var arr1 = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                var name = list[i].GetEnCnName(language);
                if (i != list.Length - 1)
                {
                    arr0.Append(name + ";");
                    arr1.Append(list[i].info + ";");
                }
                else
                {
                    arr0.Append(name);
                    arr1.Append(list[i].info);
                }
            }
            arr[0] = arr0.ToString();
            arr[1] = arr1.ToString();
            return arr;
        }
   
    }
}
