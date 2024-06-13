using AutoMapper;
using RotasApi.DTOs;
using RotasApi.Models;

namespace RotasApi.Profiles;

public class RotaProfile : Profile
{
    public RotaProfile() 
    {
        CreateMap<Rota, RotaDTO>();
    }
}
