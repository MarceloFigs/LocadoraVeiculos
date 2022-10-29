using LocadoraVeiculos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace LocadoraVeiculos.Dtos
{
    public class AlocaçãoReadDto
    {
        public string Cpf { get; set; }
        public string Chassi { get; set; }
        public DateTime DtSaida { get; set; }
        public DateTime DtEntrega { get; set; }
        public ClienteReadDto Cliente { get; set; }
        public CarroReadDto Carro { get; set; }
    }
}
