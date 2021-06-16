using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiFinance.Interface.Business;
using ApiFinance.Models;
using System.Collections.Generic;

namespace ApiFinance.Controllers
{
    [Route("v1/Pregao")]
    public class PregaoController : Controller
    {
        private readonly IPregaoBusiness _business;
        public PregaoController(IPregaoBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<ApiFinance.DTO.PregaoDto> Get()
        {
            var result = _business.BuscaPregao();
            return Ok(result);
        }

        // [HttpPost]
        [HttpGet]
        [Route("Add")]
        [AllowAnonymous]
        public ActionResult<string> AdicionarPregao()
        {
            _business.BuscaSalvarPregao();
            return Ok("Add com Sucesso");
        }

        [HttpGet]
        [Route("buscaPregao30Dias")]
        [AllowAnonymous]
        public ActionResult<List<Pregao>> BuscaAtivoBase()
        {
            var dados = _business.BuscaDadosPregao30Dias(null);
            return Ok(dados.Pregao);
        }

        [HttpGet]
        [Route("buscaDadosBase")]
        [AllowAnonymous]
        public ActionResult<Ativo> BuscaAtivoBaseTudo()
        {
            var dados = _business.BuscaTodosDadosPregao();
            return Ok(dados);
        }
    }
}