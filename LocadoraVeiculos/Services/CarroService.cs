using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.Repository;
using LocadoraVeiculos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IMapper _mapper;
        public CarroService(ICarroRepository carroRepository, IMapper mapper)
        {
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public async Task<bool> AtualizarCarro(Carro carro)
        {
            return _carroRepository.Atualizar(carro);
        }

        public async Task<CarroReadDto> BuscarCarroChassiAsync(string chassi)
        {
            var carro = await _carroRepository.BuscarPorIdAsync(chassi);
            return _mapper.Map<CarroReadDto>(carro);
        }

        public async Task<IEnumerable<CarroReadDto>> BuscarCarrosAsync()
        {
            var carro = await _carroRepository.BuscarTodosAsync();
            return _mapper.Map<IEnumerable<CarroReadDto>>(carro);
        }

        public async Task<bool> CadastrarCarro(Carro carro)
        {
            var carroExiste = await _carroRepository.BuscarPorIdAsync(carro.Chassi);
            if (carroExiste != null) return false;
            
            return _carroRepository.Incluir(carro);
        }

        public async Task<bool> ExcluirCarro(string chassi)
        {
            var carro = await _carroRepository.BuscarPorIdAsync(chassi);
            if (carro is null) return false;

            return _carroRepository.Excluir(carro);
        }
    }
}
