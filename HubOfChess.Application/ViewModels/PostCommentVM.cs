using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class PostCommentVM : IMapWith<PostComment>
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostComment, PostCommentVM>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(comment => comment.Id))
                .ForMember(vm => vm.PostId,
                    opt => opt.MapFrom(comment => comment.Post.Id))
                .ForMember(vm => vm.UserId,
                    opt => opt.MapFrom(comment => comment.User.UserId))
                .ForMember(vm => vm.Text,
                    opt => opt.MapFrom(comment => comment.Text))
                .ForMember(vm => vm.Date,
                    opt => opt.MapFrom(comment => comment.Date))
                .ForMember(vm => vm.UserFullName,
                    opt => opt.MapFrom(comment => 
                        $"{comment.User.FirstName} {comment.User.LastName}"));
        } 
    }
}
