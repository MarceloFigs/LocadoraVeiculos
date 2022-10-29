using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Profiles
{
    public class AlocaçãoProfile : Profile
    {
        public AlocaçãoProfile()
        {
            CreateMap<Alocação, AlocaçãoReadDto>();
        }
    }
}
