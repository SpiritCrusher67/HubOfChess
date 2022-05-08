using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class PostLikeVM : IMapWith<PostLike>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string UserFullName { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostLike, PostLikeVM>()
                .ForMember(vm => vm.PostId,
                    opt => opt.MapFrom(like => like.PostId))
                .ForMember(vm => vm.UserId,
                    opt => opt.MapFrom(like => like.UserId))
                .ForMember(vm => vm.Date,
                    opt => opt.MapFrom(like => like.Date))
                .ForMember(vm => vm.UserFullName,
                    opt => opt.MapFrom(like => $"{like.User.FirstName} {like.User.LastName}"));
        }
    }
}
