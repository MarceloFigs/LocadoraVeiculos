using AutoMapper;
using LocadoraVeiculos.Dtos;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            // Source -> target
            CreateMap<Cliente, ClienteReadDto>();
        }
    }
}
