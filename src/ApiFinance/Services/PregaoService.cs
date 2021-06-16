using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiFinance.Interface.Service;
using Newtonsoft.Json;

namespace ApiFinance.Services
{
    public class PregaoService : IPregaoService
    {
        private string BaseUrl => "https://query2.finance.yahoo.com";
        private string ActionPregao => "/v8/finance/chart/PETR4.SA";
        public ApiFinance.DTO.PregaoDto BuscaAtivo()
        {
            try
            {
            
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().GetAsync(this.BaseUrl + this.ActionPregao).Result;
 
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            var dados = JsonConvert.DeserializeObject<ApiFinance.DTO.PregaoDto>(response.Content.ReadAsStringAsync().Result);
           
            return dados;
 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
