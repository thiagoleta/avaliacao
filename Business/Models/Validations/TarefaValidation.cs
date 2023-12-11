using Business.Models.Validations.Documentos;
using FluentValidation;

namespace Business.Models.Validations
{
    public class TarefaValidation : AbstractValidator<Tarefa>
    {
        public TarefaValidation()
        {
            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 500)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}