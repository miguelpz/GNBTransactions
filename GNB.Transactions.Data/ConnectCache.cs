using NLog;
using System;

namespace GNB.Transactions.Data
{
    public class ConnectCache
    {
        private static string cacheData;
        private IConnect _connect;
        private string datos;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public ConnectCache(IConnect Connect)
        {
            _connect= Connect;
        }

        public void CleanCache()
        {
            cacheData = null;
        }

        public string GetData()
        {
            try
            {
                datos =  _connect.ConnectTo();               
                cacheData = datos;
                return datos;
            }
            catch(Exception ex)
            {
                if (cacheData==null || cacheData == "")
                {                   
                    logger.Debug( ex, "Error Obtain data from uri");
                    throw ex;
                }
                return cacheData;
            }
        }                             
    }        
}

