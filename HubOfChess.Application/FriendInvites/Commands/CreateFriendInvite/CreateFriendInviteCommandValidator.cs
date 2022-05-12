using FluentValidation;

namespace HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite
{
    public class CreateFriendInviteCommandValidator : AbstractValidator<CreateFriendInviteCommand>
    {
        public CreateFriendInviteCommandValidator()
        {
            RuleFor(cmd => cmd.SenderUserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.InvitedUserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.InviteMessage).MaximumLength(40);
        }
    }
}
