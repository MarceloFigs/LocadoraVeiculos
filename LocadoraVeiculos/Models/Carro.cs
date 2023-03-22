using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LocadoraVeiculos.Models
{
    public class Carro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Chassi { get; set; }        
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }

        [JsonIgnore]
        public virtual ICollection<Alocação> Alocação { get; private set; }
        [ForeignKey("Categoria")]
        public int CodCategoria { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; private set; }
    }
}
