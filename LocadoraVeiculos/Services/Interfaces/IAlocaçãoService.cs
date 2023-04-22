using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services.Interfaces
{
    public interface IAlocaçãoService
    {
        Task<bool> CadastrarAlocação(Alocação alocação);
        Task<bool> ExcluirAlocação(string cpf, string chassi);
        Task<bool> AtualizarAlocação(Alocação alocação);
        Task<AlocaçãoReadDto> BuscarAlocaçãoPorCPFeChassiAsync(string cpf, string chassi);

        Task<IEnumerable<AlocaçãoReadDto>> BuscarTodasAlocaçõesAsync();
    }
}
