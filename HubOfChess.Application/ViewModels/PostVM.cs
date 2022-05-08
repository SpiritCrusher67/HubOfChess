using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class PostVM : IMapWith<Post>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public string AuthorFullName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostVM>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(post => post.Id))
                .ForMember(vm => vm.Title,
                    opt => opt.MapFrom(post => post.Title))
                .ForMember(vm => vm.Text,
                    opt => opt.MapFrom(post => post.Text))
                .ForMember(vm => vm.AuthorId,
                    opt => opt.MapFrom(post => post.Author.UserId))
                .ForMember(vm => vm.AuthorFullName,
                    opt => opt.MapFrom(post => $"{post.Author.FirstName} {post.Author.LastName}"))
                .ForMember(vm => vm.Date,
                    opt => opt.MapFrom(post => post.Date))
                .ForMember(vm => vm.LikesCount,
                    opt => opt.MapFrom(post => post.Likes.Count))
                .ForMember(vm => vm.CommentsCount,
                    opt => opt.MapFrom(post => post.Comments.Count));
        }
    }
}
