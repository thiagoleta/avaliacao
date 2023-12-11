using System.ComponentModel.DataAnnotations;

namespace Api.Commands
{
    public class CadastroTarefaCommand
    {
        public string Titulo { get; set; }

        public bool Concluida { get; set; }
    }
}
