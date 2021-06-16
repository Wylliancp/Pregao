using System;
using ApiFinance.Models;

namespace ApiFinance.Interface.Business
{
    public interface IPregaoBusiness
    {
        DTO.PregaoDto BuscaPregao();
        void BuscaSalvarPregao();
        Ativo BuscaDadosPregao30Dias(DateTime? date);
        Ativo BuscaTodosDadosPregao();
    }
}