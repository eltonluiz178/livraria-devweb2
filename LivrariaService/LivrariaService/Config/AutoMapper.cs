using AutoMapper;
using Domain.Dtos;
using Domain.Model;

namespace LivrariaService.Config;

public class AutoMapper: Profile
{
    public AutoMapper()
    {
        CreateMap<CriarEmprestimoDto, Emprestimo>().ReverseMap();
    }
}
