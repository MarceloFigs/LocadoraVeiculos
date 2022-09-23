using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections;
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
        public string Endereço { get; set; }

        [JsonIgnore]
        public virtual ICollection<Alocação> Alocação { get; private set; }
    }
}
