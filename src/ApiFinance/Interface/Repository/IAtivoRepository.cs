using System;
using ApiFinance.Models;

namespace ApiFinance.Interface.Repository
{
    public interface IAtivoRepository
    {
        void AicionarAtivo(Ativo ativo);        
        bool ExisteAtivo(Ativo ativo);  
        Ativo BuscarAtivoPorPeriodo(DateTime date);        
        Ativo BuscarTodosAtivo();        

    }
}