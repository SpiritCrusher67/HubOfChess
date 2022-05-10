using FluentValidation;

namespace HubOfChess.Application.Messages.Queries.GetMessagesByChatId
{
    public class GetMessagesByChatIdQueryValidator : AbstractValidator<GetMessagesByChatIdQuery>
    {
        public GetMessagesByChatIdQueryValidator()
        {
            RuleFor(query => query.ChatId).NotNull().NotEqual(Guid.Empty);
            RuleFor(query => query.UserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(query => query.Page).GreaterThanOrEqualTo(1);
            RuleFor(query => query.PageLimit).GreaterThanOrEqualTo(0);
        }
    }
}
