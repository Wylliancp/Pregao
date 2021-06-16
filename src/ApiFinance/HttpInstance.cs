using System.Net.Http;


namespace ApiFinance
{
    public class HttpInstance
    {
        private static HttpClient _httpClient;
        
        public static HttpClient GetHttpClientInstance() 
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            }    
            return _httpClient;
        }
            
    }
}
