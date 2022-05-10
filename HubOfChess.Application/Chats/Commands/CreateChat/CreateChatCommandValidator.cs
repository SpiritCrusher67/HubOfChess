using FluentValidation;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(cmd => cmd.ChatOwner).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.ChatName).MaximumLength(20);
        }
    }
}
