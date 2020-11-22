using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace TheProxor.AbTest.Connection
{
    public static class AbTestResponseHandler
    {
        [Serializable]
        private class RequestData
        {
            private string host;
            private string projectID;
            private string password;

            public RequestData(string _host, string _projectID, string _password)
            {
                host = _host;
                projectID = _projectID;
                password = _password;
            }

            public override string ToString()
            {
                return $"{host}/{JsonUtility.ToJson(this)}";
            }
        }

        private static RequestData requestData;

        private static string uri => requestData.ToString();

        public static void Initialize(string _host, string _projectID, string _password)
        {
            requestData = new RequestData(_host, _projectID, _password);
        }

        public static string Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static async Task<string> GetAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}