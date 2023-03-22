using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocadoraVeiculos.Models
{
    public class Categoria
    {
        [Key]
        [JsonIgnore]
        public int CodCategoria { get; set; }        
        public string Descrição { get; set; }
        public double ValorDiaria { get; set; }
        [JsonIgnore]
        public virtual ICollection<Carro> Carro { get; private set; }
    }
}
