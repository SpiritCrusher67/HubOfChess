using FluentValidation;

namespace HubOfChess.Application.Chats.Commands.UpdateChat
{
    public class UpdateChatCommandValidator : AbstractValidator<UpdateChatCommand>
    {
        public UpdateChatCommandValidator()
        {
            RuleFor(cmd => cmd.ChatId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.ChatName).MaximumLength(20);
        }
    }
}
