using Api.Commands;
using Api.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/tarefas")]   
    public class TarefaController : MainController
    {
        private readonly ITarefaService _tarefaService;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;

        public TarefaController(INotificador notificador,
                                  
                                  ITarefaService tarefaService,
                                  ITarefaRepository tarefaRepository,
                                  IMapper mapper) : base(notificador)
        {
            _tarefaService = tarefaService;
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
        }

        [HttpPost("adicionar-tarefa")]
        public async Task<ActionResult<CadastroTarefaCommand>> Adicionar(CadastroTarefaCommand command)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tarefaService.Adicionar(_mapper.Map<Tarefa>(command));
            

            return CustomResponse(command);
        }

        [HttpPut("atualizar-tarefa/{id:int}")]
        public async Task<ActionResult<AlterarTarefaCommand>> Atualizar(int id, AlterarTarefaCommand command)
        {

            if (id != command.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(command);
            }

            var buscarTarefa = await _tarefaRepository.BuscarPorID(id);
            if (buscarTarefa == null )
            {
                NotificarErro("O id da tarefa Não existe na base de dados!");
                return CustomResponse(command);
            }

            if (buscarTarefa.Concluida )
            {
                NotificarErro("A tarefa só pode ser atualizada se não estiver como concluída!");
                return CustomResponse(command);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tarefaService.Atualizar(_mapper.Map<Tarefa>(command));

            return CustomResponse(command);
        }


        [HttpDelete("excluir/{id:int}")]
        public async Task<ActionResult<TarefaViewModel>> Excluir(int id)
        {
            var tarefa = await _tarefaRepository.ObterPorId(id);

            if (tarefa == null) return NotFound();

            if (tarefa.Concluida == true)
            {
                await _tarefaService.Remover(id);
                return CustomResponse(tarefa);
            }
            NotificarErro("Uma tarefa só pode ser removida se estiver concluída!");
            return CustomResponse(true);

        }

        [HttpGet("obter-tarefas-nao-concluidas")]
        public async Task<IEnumerable<TarefaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TarefaViewModel>>(await _tarefaRepository.BuscarTodasTarefasNaoConcluidas());
        }


        [HttpGet("buscar-por-id-tarefa/{id}")]
        public async Task<TarefaViewModel> ObterUsuario(int id)
        {
            return _mapper.Map<TarefaViewModel>(await _tarefaRepository.ObterPorId(id));
        }

    }

}
