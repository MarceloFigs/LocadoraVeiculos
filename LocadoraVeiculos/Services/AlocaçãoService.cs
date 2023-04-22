using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class AlocaçãoService : IAlocaçãoService
    {
        private readonly IAlocaçãoRepository _alocaçãoRepository;
        private readonly IMapper _mapper;

        public AlocaçãoService(IAlocaçãoRepository alocaçãoRepository, IMapper mapper)
        {
            _alocaçãoRepository = alocaçãoRepository;
            _mapper = mapper;
        }

        public async Task<bool> AtualizarAlocação(Alocação alocação)
        {
            return _alocaçãoRepository.Atualizar(alocação);
        }

        public async Task<AlocaçãoReadDto> BuscarAlocaçãoPorCPFeChassiAsync(string cpf, string chassi)
        {
            var alocação = await _alocaçãoRepository.BuscarAlocaçãoPorCPFeChassi(cpf, chassi);
            return _mapper.Map<AlocaçãoReadDto>(alocação);
        }

        public async Task<IEnumerable<AlocaçãoReadDto>> BuscarTodasAlocaçõesAsync()
        {
            var alocação = await _alocaçãoRepository.BuscarTodosAsync();
            return _mapper.Map<IEnumerable<AlocaçãoReadDto>>(alocação);
        }

        public async Task<bool> CadastrarAlocação(Alocação alocação)
        {
            var alocaçãoExiste = await _alocaçãoRepository.BuscarAlocaçãoPorCPFeChassi(alocação.Cpf, alocação.Chassi);
            if (alocaçãoExiste != null) return false;

            return _alocaçãoRepository.Incluir(alocação);
        }

        public async Task<bool> ExcluirAlocação(string cpf, string chassi)
        {
            var alocação = await _alocaçãoRepository.BuscarAlocaçãoPorCPFeChassi(cpf, chassi);
            if (alocação is null) return false;

            return _alocaçãoRepository.Excluir(alocação);
        }
    }
}
