using System;

namespace LocadoraVeiculos.Dtos
{
    public class ClienteReadDto
    {
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
