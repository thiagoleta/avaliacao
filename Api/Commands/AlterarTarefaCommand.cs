using System.ComponentModel.DataAnnotations;

namespace Api.Commands
{
    public class AlterarTarefaCommand
    {       
        public int Id { get; set; }
     
        public string Titulo { get; set; }
       
        public bool Concluida { get; set; }     

    }
}
