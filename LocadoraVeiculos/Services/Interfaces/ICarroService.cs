using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services.Interfaces
{
    public interface ICarroService
    {
        Task<bool> CadastrarCarro(Carro carro);
        Task<bool> ExcluirCarro(string chassi);
        Task<bool> AtualizarCarro(Carro carro);
        Task<CarroReadDto> BuscarCarroChassiAsync(string chassi);

        Task<IEnumerable<CarroReadDto>> BuscarCarrosAsync();
    }
}
