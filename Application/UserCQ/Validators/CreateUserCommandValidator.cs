using Application.UserCQ.Commands;
using FluentValidation;

namespace Application.UserCQ.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O campo de nome não pode ser vazio"); 
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo de email não pode ser vazio");   
            RuleFor(x=> x.Password).NotEmpty().WithMessage("O campo de senha não pode ser vazio"); 

        }
    }
}
