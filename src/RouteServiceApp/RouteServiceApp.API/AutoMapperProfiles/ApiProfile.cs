using AutoMapper;
using RouteServiceApp.API.Locality.Requests;
using RouteServiceApp.API.Locality.Responses;
using RouteServiceApp.API.Route.Requests;
using RouteServiceApp.API.Route.Responses;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.AutoMapperProfiles
{
	public class ApiProfile : Profile
	{
		public ApiProfile()
		{
			/* Examples:
			CreateMap<User, ClientDTO>();
			CreateMap<UserForRegistrationDto, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
				.ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Name));


			CreateMap<Ride, RideDto>().ForMember(x => x.Orders, opt => opt.Ignore());
			CreateMap<User, UserDto>().ForMember(userDto => userDto.Rides, opt => opt.MapFrom(ride => ride.Rides));
			*/


			// Request => Dto
			CreateMap<LocalityRequest, LocalityDto>();

			CreateMap<RouteLocalityRequest, RouteLocalityDto>().AfterMap((routeLocReq, routeLocDto, context) => routeLocDto.Locality = context.Mapper.Map<LocalityDto>(routeLocReq.Locality));
			CreateMap<CreateRouteRequest, CreateUpdateRouteDto>().AfterMap((routeReq, routeDto, context) => routeDto.RouteLocalities = context.Mapper.Map<List<RouteLocalityDto>>(routeReq.RouteLocalities));
			CreateMap<UpdateRouteRequest, CreateUpdateRouteDto>().AfterMap((routeReq, routeDto, context) => routeDto.RouteLocalities = context.Mapper.Map<List<RouteLocalityDto>>(routeReq.RouteLocalities));

			// Dto => Response
			CreateMap<LocalityDto, LocalityResponse>();
			CreateMap<RouteDto, RouteResponse>();
			CreateMap<GetRouteDto, GetRouteResponse>();
			CreateMap<RouteLocalityDto, RouteLocalityResponse>();

		}
	}
}
