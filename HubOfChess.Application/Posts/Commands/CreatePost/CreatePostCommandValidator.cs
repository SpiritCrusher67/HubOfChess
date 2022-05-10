using FluentValidation;

namespace HubOfChess.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(cmd => cmd.UserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(cmd => cmd.Title).NotEmpty().MaximumLength(40);
            RuleFor(cmd => cmd.Text).NotEmpty().MaximumLength(1000);
        }
    }
}
