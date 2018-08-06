using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using GBI.Metrix.Utility;
using KolTool.Base;
using Newtonsoft.Json;

namespace GBI.Metrix.Service.Misc
{
    public static class MiscService
    {
        public static List<MiscEntity> GetMiscList(this MiscEnum misc)
        {
            return GetMisc(misc);
        }

        public static List<MiscEntity> GetMisc(MiscEnum misc)
        {
            var memoryCache = MemoryCache.Default;
            string key = string.Format("Get_{0}", misc.ToString());
            if (!memoryCache.Contains(key))
            {
                var result = JsonHelper.FromJson<List<MiscEntity>>(JanusClientHelper.GetMisc(misc));
                memoryCache.Add(key, result, new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable });
            }
            return memoryCache.Get(key, null) as List<MiscEntity>;
        }

        

        public static List<MiscEntity> GetAllAreas()
        {
            var memoryCache = MemoryCache.Default;
            string key = "Get_AllArea";
            if (!memoryCache.Contains(key))
            {
                var province = JsonHelper.FromJson<List<MiscEntity>>(JanusClientHelper.GetMisc(MiscEnum.china_province));
                var city = JsonHelper.FromJson<List<MiscEntity>>(JanusClientHelper.GetMisc(MiscEnum.china_city));
                var result = new List<MiscEntity>();
                result.AddRange(province);
                result.AddRange(city);
                memoryCache.Add(key, result, new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable });
            }
            return memoryCache.Get(key, null) as List<MiscEntity>;
        }

        public static string GetMiscName(this string miscValue,MiscEnum miscEnum,string language)
        {
            if (string.IsNullOrWhiteSpace(miscValue)) return "";
            List<MiscEntity> listMisc;
            if (miscEnum.Equals(MiscEnum.allarea))
            {
                listMisc = GetAllAreas();
            }
            else
            {
                listMisc = miscEnum.GetMiscList();
            }
            var entity = listMisc.Where(s => s.dataid.Equals(miscValue)).FirstOrDefault();
            if (entity != null)
                return LanguagePriorityHelper.GeneratePriorityLanguage(language, entity.name_en, entity.name_cn);
            return "";
        }

        public static string GetMiscName(this MiscEntity entity, string language)
        {
            if (entity == null) return "";
            return LanguagePriorityHelper.GeneratePriorityLanguage(language, entity.name_en, entity.name_cn);
        }

        public static string GetMiscNameList(this string[] miscValues, MiscEnum miscEnum, string language)
        {
            string result = "";
            if (miscValues == null || !miscValues.Any()) return "";
            miscValues.ToList().ForEach(t =>
            {
                result += !string.IsNullOrEmpty(t.GetMiscName(miscEnum, language))
                    ? t.GetMiscName(miscEnum, language) + ", "
                    : "";
            });
            result = result.Trim().TrimEnd(',').TrimEnd(';');
            return result;
        }

 
        public static List<string> GetLevelNameByIds(string[] ids, MiscEnum miscEnum, string language)
        {
            if (ids == null || !ids.Any())
                return null;

            var list = new List<string>();
            ids.ToList().ForEach(t =>
            {
                list.Add(t.GetMiscName(miscEnum, language));
            });
            return list;
        }

    
    }
}
