using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models
{
    public class Alocação
    {
        public string Cpf { get; set; }
        public string Chassi { get; set; }
        public DateTime HrSaida { get; set; }
        public DateTime DtSaida { get; set; }
        public DateTime HrEntrega { get; set; }
        public DateTime DtEntrega { get; set; }

        [JsonIgnore]
        [ForeignKey("Cpf")]
        public Cliente Cliente { get; set; }
        [JsonIgnore]
        [ForeignKey("Chassi")]
        public Carro Carro { get; set; }
    }
}
