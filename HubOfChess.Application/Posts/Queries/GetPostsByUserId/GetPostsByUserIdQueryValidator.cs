using FluentValidation;

namespace HubOfChess.Application.Posts.Queries.GetPostsByUserId
{
    public class GetPostsByUserIdQueryValidator : AbstractValidator<GetPostsByUserIdQuery>
    {
        public GetPostsByUserIdQueryValidator()
        {
            RuleFor(query => query.UserId).NotNull().NotEqual(Guid.Empty);
            RuleFor(query => query.Page).GreaterThanOrEqualTo(1);
            RuleFor(query => query.PageLimit).GreaterThanOrEqualTo(0);
        }
    }
}
