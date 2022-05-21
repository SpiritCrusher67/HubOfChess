using AutoMapper;
using HubOfChess.Application.Common.Mappings;

namespace HubOfChess.WebApi.Models
{
    public abstract class BaseDtoModel<MapWithT> : IMapWith<MapWithT>
    {
        protected Guid userId;
        public void SetUserId(Guid id) => userId = id;

        public abstract void Mapping(Profile profile);
    }
}
