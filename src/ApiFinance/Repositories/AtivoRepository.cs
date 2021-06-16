using System;
using System.Linq;
using ApiFinance.Data;
using ApiFinance.Interface.Repository;
using ApiFinance.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinance.Repositories
{
    public class AtivoRepository : IAtivoRepository
    {
        private readonly DataContext _context;
        
        public AtivoRepository(DataContext context)
        {
            _context = context;
        }

        public void AicionarAtivo(Ativo ativo)
        {
            // if (!ExisteAtivo(ativo)) 
            
            _context.Ativos.Add(ativo);

            _context.SaveChanges();
        }

        public Ativo BuscarAtivoPorPeriodo(DateTime date)
        {
            var pregao = _context.Pregaos.Where(x => x.TimesTamp.Date >= date.Date).ToList();

            var ativo = new Ativo();
            ativo.AddPregoes(pregao);
            return ativo;
        }

        public Ativo BuscarTodosAtivo()
        {
            var ativos = _context.Ativos.Include(x => x.Pregao).ToList().OrderBy(x => x.Id);

            var ativo = new Ativo();
            foreach(var itemAtivo in ativos)
            {
                ativo.AddPregoes(itemAtivo.Pregao);
            }
            return ativo;
        }

        public bool ExisteAtivo(Ativo ativo)
        { 
            var result = _context.Ativos.OrderByDescending(x => x.Id);
            return result.Count() >= 1 ? true : false;
        }
    }
}