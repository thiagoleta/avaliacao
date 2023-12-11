using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;

namespace Business.Services
{
    public class TarefaService : BaseService, ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository,                    
                              INotificador notificador) : base(notificador)
        {
            _tarefaRepository = tarefaRepository;          
        }       

        public async Task Adicionar(Tarefa usuario)
        {
            if (!ExecutarValidacao(new TarefaValidation(), usuario)) return;

            await _tarefaRepository.Adicionar(usuario);
        }

        public async Task Atualizar(Tarefa usuario)
        {
            if (!ExecutarValidacao(new TarefaValidation(), usuario)) return;

            await _tarefaRepository.Atualizar(usuario);
        }
     

        public void Dispose()
        {
            _tarefaRepository?.Dispose();
        }

        public async Task Remover(int id)
        {
            await _tarefaRepository.Deletar (id);
        }



    }
}
