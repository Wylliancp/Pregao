using System;
using System.ComponentModel.DataAnnotations;

namespace ApiFinance.Models
{
     public class Pregao 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório - TimesTamp")]
        public DateTime TimesTamp { get; set; }
        public double? Open { get; set; }
        public double? Close { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }
        public double? Volume { get; set; }
        public string VariacaoD1 { get; set; }
        public string VariacaoDAnt { get; set; }
    }
}