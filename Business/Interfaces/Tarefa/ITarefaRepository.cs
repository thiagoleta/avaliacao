using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task Deletar(int id);

         Task<Tarefa> BuscarPorID(int id);

        Task<IEnumerable<Tarefa>> BuscarTodasTarefasNaoConcluidas();
    }
}
