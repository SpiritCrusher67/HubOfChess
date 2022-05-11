using FluentValidation;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(cmd => cmd.ChatOwnerUserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.ChatName).MaximumLength(20);
        }
    }
}
