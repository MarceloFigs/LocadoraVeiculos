using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Profiles
{
    public class CarroProfile : Profile
    {
        public CarroProfile()
        {
            CreateMap<Carro, CarroReadDto>();
        }
    }
}
