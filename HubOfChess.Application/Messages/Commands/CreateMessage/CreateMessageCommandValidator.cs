using FluentValidation;

namespace HubOfChess.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(cmd => cmd.ChatId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.UserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.Text).NotEmpty().MaximumLength(200);
        }
    }
}
