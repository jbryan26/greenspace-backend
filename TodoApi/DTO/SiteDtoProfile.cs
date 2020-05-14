using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using TodoApi.Models;
using Site = TodoApi.Models.Site;

namespace TodoApi.DTO
{
    public class SiteDtoProfile : AutoMapper.Profile
    {
        public SiteDtoProfile()
        {
            CreateMap<Site, SiteDto>();
            CreateMap<Building, BuildingDto>();
            CreateMap<Floor, FloorDto>();
            CreateMap<Room, RoomDto>().ForMember(dest => dest.ResourceTypeName, opt => opt.MapFrom(src
                => src.ResourceTypeId.HasValue ? src.ResourceType.Name : "room"));
            CreateMap<ReservationModel, ReservationDto>();

            // Add other CreateMap’s for any other configs
        }
    }
}
