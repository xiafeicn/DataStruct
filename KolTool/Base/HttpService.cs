using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using GBI.Metrix.Service;
using GBI.Metrix.Utility;
using KolTool.Model;
using Newtonsoft.Json.Linq;

namespace KolTool.Base
{
    public class HttpService
    {
        public static List<MiscEntity> GetNameListByTable(DataSetEnum dataSet, string ids)
        {
            var memoryCache = MemoryCache.Default;
            string key = string.Format("Get_{0}", ids.ToString());
            if (!memoryCache.Contains(key))
            {
                var expiration = DateTimeOffset.UtcNow.AddDays(1);
                string url = JanusClientHelper.DataService + string.Format("list/names?table={0}&ids={1}", dataSet.ToString(), ids);
                var result = JsonHelper.FromJson<List<MiscEntity>>(JObject.Parse(HttpClientHolder.GetRequest(url))["data"].ToString());
                memoryCache.Add(key, result, expiration);
            }
            return memoryCache.Get(key, null) as List<MiscEntity>;
        }


        
        public static T GetByIds<T>(string[] ids, DataSetEnum moudle) where T : new()
        {
            if (!ids.Any())
                return new T();
            var search = JanusHelper.GeneralIdsSearch(moudle, ids.ToList());
            var url = JanusClientHelper.DataService + search;
            return JsonHelper.FromJson<T>(HttpClientHolder.GetRequest(url));
        }


        public static T GetById<T>(object id, DataSetEnum moudle)
        {
            var memoryCache = MemoryCache.Default;
            string key = string.Format("Get_{0}_{1}", typeof(T).ToString(), id.ToString());
            if (!memoryCache.Contains(key))
            {
                var search = JanusHelper.GeneralIdsSearch(moudle, new string[] { id.ToString() }.ToList());
                search = search + "&aggregations=paper_count,grant_count,clinicaltrial_count";
                var url = JanusClientHelper.DataService + search;
                var expiration = DateTimeOffset.UtcNow.AddDays(1);
                var result = JsonHelper.FromJson<T>(HttpClientHolder.GetRequest(url));
                memoryCache.Add(key, result, expiration);
            }
            return (T)memoryCache.Get(key, null);
        }

        public static ResearcherEntity GetHcpById(string id)
        {
            return GetById<BaseEntity<ResearcherEntity>>(id, DataSetEnum.hcp).data.FirstOrDefault();
        }

    }
}