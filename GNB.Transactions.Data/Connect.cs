using System;
using System.IO;
using System.Net;

namespace GNB.Transactions.Data
{
    public class Connect:IConnect
    {
        protected string connectionString;

        public Connect(String uri)
        {
            this.connectionString = uri;
        }

        public string ConnectTo()
        {
            string json;

            HttpWebRequest RateRequest = (HttpWebRequest)WebRequest.Create(connectionString);

            using (HttpWebResponse response = (HttpWebResponse)RateRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }        
    }
}
