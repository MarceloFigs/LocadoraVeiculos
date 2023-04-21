using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ICEPService _iCepService;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper,
            ICEPService iCepService)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _iCepService = iCepService;
        }

        public async Task<IEnumerable<ClienteReadDto>> BuscarClientesAsync()
        {
            var cliente = await _clienteRepository.BuscarTodosAsync();
            return _mapper.Map<IEnumerable<ClienteReadDto>>(cliente);
        }

        public async Task<ClienteReadDto> BuscarClienteCPFAsync(string cpf)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(cpf);
            return _mapper.Map<ClienteReadDto>(cliente);
        }

        public async Task<bool> CadastrarCliente(Cliente cliente)
        {
            var clienteExiste = await _clienteRepository.BuscarPorIdAsync(cliente.Cpf);
            if (clienteExiste != null) return false;

            var endereco = await _iCepService.BuscarCEP(cliente.CEP);
            if (endereco.UF is null) return false;

            _iCepService.AtribuirCep(cliente, endereco);
            return _clienteRepository.Incluir(cliente);
        }

        public async Task<bool> AtualizarCliente(Cliente cliente)
        {
            var endereco = await _iCepService.BuscarCEP(cliente.CEP);
            if (endereco.UF is null) return false;

            _iCepService.AtribuirCep(cliente, endereco);
            return _clienteRepository.Atualizar(cliente);
        }

        public async Task<bool> ExcluirCliente(string cpf)
        {
            var cliente = await _clienteRepository.BuscarPorIdAsync(cpf);
            if (cliente is null) return false;
            
            return _clienteRepository.Excluir(cliente);
        }
    }
}
