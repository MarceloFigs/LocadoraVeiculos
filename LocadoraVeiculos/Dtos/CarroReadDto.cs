using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Dtos
{
    public class CarroReadDto
    {
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }        
        //public virtual ICollection<Alocação> Alocação { get; private set; }
        public int CodCategoria { get; set; }        
        public virtual Categoria Categoria { get; private set; }
    }
}
