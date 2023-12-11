using Api.Commands;
using Api.ViewModels;
using AutoMapper;
using Business.Models;

namespace Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Tarefa, TarefaViewModel>().ReverseMap();
            CreateMap<Tarefa, AlterarTarefaCommand>().ReverseMap();
            CreateMap<Tarefa, CadastroTarefaCommand>().ReverseMap();            

        }
    }
}