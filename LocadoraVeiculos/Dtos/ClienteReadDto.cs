using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Dtos
{
    public class ClienteReadDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Cpf { get; set; }
        public string RG { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Cnh { get; set; }
        public string Nome { get; set; }
        public int CEP { get; set; }        
        public string UF { get; set; }        
        public string Cidade { get; set; }        
        public string Logradouro { get; set; }
    }
}
