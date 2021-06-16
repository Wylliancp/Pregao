using System;
using System.Collections.Generic;
using System.Linq;
using ApiFinance.Interface.Business;
using ApiFinance.Interface.Repository;
using ApiFinance.Interface.Service;
using ApiFinance.Models;
using ApiFinance.Utils;

namespace ApiFinance.Business
{
    public class PregaoBusiness : IPregaoBusiness
    {
        private static DateTime Pregao30Dias = DateTime.Now.AddDays(-30); 
        private readonly IPregaoService _service;
        private readonly IAtivoRepository _repository;
        public PregaoBusiness(IPregaoService service, IAtivoRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public DTO.PregaoDto BuscaPregao()
        {
            var pregao =_service.BuscaAtivo();

            if(pregao == null) return null;

            return pregao;
        }
        public void BuscaSalvarPregao()
        {
            var pregao =_service.BuscaAtivo();

            if(pregao == null) return;

            AdicionarAtivos(pregao);
        }

        public Ativo BuscaDadosPregao30Dias(DateTime? date)
        {
            if(date == null) date = Pregao30Dias;

            return _repository.BuscarAtivoPorPeriodo((DateTime)date);
        }

        private void AdicionarAtivos(DTO.PregaoDto pregao)
        {
            foreach(var item in pregao.chart.result)
            {
              AdicionarTimesTempOpen(item);
            }

        }
        

        private void AdicionarTimesTempOpen(DTO.Result itemResult)
        {
            var ativo = new Ativo();
            var timestamp = new List<DateTime>();  
            foreach(var itemData in itemResult.timestamp)
            {
                timestamp.Add(itemData.TimeStampToDateTime());
            }   

            var open    = new List<double?>();
            var close   = new List<double?>();
            var low     = new List<double?>();
            var high    = new List<double?>();
            var volume  = new List<int?>();
            
            foreach(var itemQuote in itemResult.indicators.quote)
            {
              open.AddRange(itemQuote.open);
              close.AddRange(itemQuote.close);
              low.AddRange(itemQuote.low);
              high.AddRange(itemQuote.high);
              volume.AddRange(itemQuote.volume); 
            }   
            
            for(int i = 0; i < timestamp.Count(); i++)
            {
                var openOld = i == 0 ? 0 : open[i - 1];
                var openOne = open[0];
                var variacaoDAnt = (open[i] / openOld - 1) *100;
                var variacaoD1 = (open[i] - openOld - 1) *100;
                string variacaoAntt = variacaoDAnt + "%";
                string variacaoD11 = variacaoD1 + "%";
                var pregao = new Pregao()
                {
                    TimesTamp = timestamp[i],
                    Open = open[i],
                    Close = close[i],
                    Low = low[i],
                    High = high[i],
                    Volume = (double?)volume[i],
                    VariacaoDAnt = variacaoAntt,
                    VariacaoD1 = variacaoD11
                };
                 ativo.AddPregao(pregao);
            }

            _repository.AicionarAtivo(ativo);      
        }

        public Ativo BuscaTodosDadosPregao()
        {
            return _repository.BuscarTodosAtivo();
        }
    }
}



// var pregaoFinal = new List<Pregao>();
//             foreach(var item in pregao.chart.result)
//             {
//              var datas = new List<DateTime>();
//              foreach(var itemData in pregao.chart.result[item.timestamp.Count].timestamp)
//              {
//                  datas.Add(itemData.TimeToDateTime());
//                 var open = new List<double?>();
//                 foreach(var itemOpen in pregao.chart.result[item.timestamp.Count].indicators.quote[item.timestamp.Count].open)
//                 {
//                     open.Add(itemOpen);
//                     var pregaoAux = new Pregao()
//                     {
//                         TimesTamp = itemData.TimeToDateTime(),
//                         Open = itemOpen 
//                     };
//                      var ativo = new Ativo();
//                      ativo.AddPregao(pregaoAux);
//                      _repository.AicionarAtivo(ativo);
//                 }
//              }          
//             }