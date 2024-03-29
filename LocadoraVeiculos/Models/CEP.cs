﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models
{
    [NotMapped]
    public class CEP
    {
        public string UF { get; set; }
        [JsonProperty("localidade")]
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
    }
}
