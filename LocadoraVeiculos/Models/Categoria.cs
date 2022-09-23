using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models
{
    public class Categoria
    {
        [Key]
        public int CodCategoria { get; set; }
        public string Descrição { get; set; }
        public double ValorDiaria { get; set; }
        [JsonIgnore]
        public Carro Carro { get; private set; }
    }
}
