using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models
{
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Cpf { get; set; }
        public string RG { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Cnh { get; set; }
        public string Nome { get; set; }
        public int CEP { get; set; }
        [JsonIgnore]
        public string UF { get; set; }
        [JsonIgnore]
        public string Cidade { get; set; }
        [JsonIgnore]
        public string Logradouro { get; set; }

        [JsonIgnore]
        public virtual ICollection<Alocação> Alocação { get; private set; }
    }
}
