using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFinance.Models
{
    public class Ativo
    {
        public Ativo()
        {
            Pregao = new List<Pregao>();    
        }

        [Key]
        public int Id { get; set; }

        public List<Pregao> Pregao { get; set; }

        public void AddPregoes(List<Pregao> pregaos)
        { 
            foreach(var item in pregaos)
            {
                AddPregao(item);
            }
        }

        public void AddPregao(Pregao pregao)
        { 
          if(pregao == null) return;
          
          Pregao.Add(pregao);
          
        }
        
    }

}