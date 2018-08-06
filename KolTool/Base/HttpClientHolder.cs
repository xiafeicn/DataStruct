using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KolTool.Base
{
    public static class HttpClientHolder
    {
        public static string GetRequest(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                var result = sr.ReadToEnd();
                Debug.WriteLine(url);
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    switch (((System.Net.HttpWebResponse)(ex.Response)).StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedAccessException("UnauthorizedAccess", ex);
                        default:
                            throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            finally
            {

            }
        }


        public async static Task<string> GetRequestAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    return await httpClient.GetStringAsync(url).ConfigureAwait(false);
                }
                catch (WebException ex)
                {
                    if (ex.Response == null) throw;
                    switch (((HttpWebResponse)(ex.Response)).StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            throw new UnauthorizedAccessException("UnauthorizedAccess", ex);
                        default:
                            throw;
                    }
                }
            }


            //try
            //{
            //    WebClient client = new WebClient();
            //    client.Headers.Add("Content-Type", "application/json");
            //    Stream stream = client.OpenRead(url);
            //    //var length = stream.Length;
            //    StreamReader reader = new StreamReader(stream);
            //    return reader.ReadToEnd();
            //}
            //catch (WebException ex)
            //{
            //    if (ex.Response != null)
            //    {
            //        switch (((System.Net.HttpWebResponse)(ex.Response)).StatusCode)
            //        {
            //            case HttpStatusCode.Unauthorized:
            //                throw new UnauthorizedAccessException("UnauthorizedAccess", ex);
            //            default:
            //                throw;
            //        }
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }

        public static string Post(string url, string postData)
        {
            byte[] bs = Encoding.ASCII.GetBytes(postData);
            var req = (System.Net.HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 90000;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            req.AllowAutoRedirect = false;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        public static void GetPost(string url, string postData)
        {
            try
            {
                WebClient client = new WebClient();
                byte[] sendData = Encoding.GetEncoding("utf-8").GetBytes(postData);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", sendData.Length.ToString());
                byte[] recData = client.UploadData(url, "POST", sendData);
            }
            catch (WebException)
            {
                throw;
            }
        }

        public static string GetRequestForExport(string url)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                Stream stream = client.OpenRead(url);
                //var length = stream.Length;
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (WebException)
            {
                return "0";
            }
        }
    }
}
