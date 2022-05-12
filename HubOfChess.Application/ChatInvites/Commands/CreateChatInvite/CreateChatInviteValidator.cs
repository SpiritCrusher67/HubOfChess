using FluentValidation;

namespace HubOfChess.Application.ChatInvites.Commands.CreateChatInvite
{
    public class CreateChatInviteValidator : AbstractValidator<CreateChatInviteCommand>
    {
        public CreateChatInviteValidator()
        {
            RuleFor(cmd => cmd.ChatId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.SenderUserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.InvitedUserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.InviteMessage).MaximumLength(40);
        }
    
    }
}
