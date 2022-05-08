using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class UserVM : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime BirthDate { get; set; }
        public string? AboutMe { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVM>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(user => user.UserId))
                .ForMember(vm => vm.FirstName,
                    opt => opt.MapFrom(user => user.FirstName))
                .ForMember(vm => vm.LastName,
                    opt => opt.MapFrom(user => user.LastName))
                .ForMember(vm => vm.BirthDate,
                    opt => opt.MapFrom(user => user.BirthDate))
                .ForMember(vm => vm.AboutMe,
                    opt => opt.MapFrom(user => user.AboutMe));
        }
    }
}
