using System;
using System.Configuration;
using System.Threading.Tasks;

namespace KolTool.Base
{
    public static class JanusClientHelper
    {
        public static string DataService
        {
            get
            {
                return ConfigurationManager.AppSettings["DataService"];
            }
        }

        public static string DataList(string search)
        {
            string url = JanusClientHelper.DataService +
                string.Format("list?{0}", search);

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }

        public static string GetStatistic(string search)
        {
            string url = JanusClientHelper.DataService +
                string.Format("facet?{0}", search);

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }



        public static string GetCTDetailSearch(string id)
        {
            string url = JanusClientHelper.DataService +
                string.Format("list/ct?id={0}", id);

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }

        public static string DiseaseList(string search, bool withPage)
        {
            var url = string.Empty;
            if (withPage)
                url = DataService + string.Format("list/?{0}", search);
            else
                url = DataService + string.Format("list/disease?{0}", search);

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }

        public static string QueryResearcher(string search)
        {
            var url = DataService + string.Format("query?{0}", search);

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }
        public static string GetMisc(MiscEnum misc)
        {
            string requestId = Guid.NewGuid().ToString();
            string url = JanusClientHelper.DataService + string.Format("reference/" + misc.ToString());

            var result = HttpClientHolder.GetRequest(url);
            return result;
        }
        public static string GetJanusResult(string url)
        {
            url = string.Concat(DataService, url);
            var result = HttpClientHolder.GetRequest(url);
            return result;
        }

        public static async Task<string> GetJanusResultAsync(string url)
        {
            url = string.Concat(DataService, url);
            var result = await HttpClientHolder.GetRequestAsync(url);
            return result;
        }
    }
}
