using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITarefaService : IDisposable
    {
        Task Adicionar(Tarefa tarefa);
        Task Atualizar(Tarefa tarefa);
        Task Remover(int id); 
    }
}