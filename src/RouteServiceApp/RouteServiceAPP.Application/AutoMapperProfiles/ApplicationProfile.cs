using AutoMapper;
using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceAPP.Application.AutoMapperProfiles
{
	public class ApplicationProfile : Profile
	{
		public ApplicationProfile()
		{
			/* Examples:
			CreateMap<User, ClientDTO>();
			CreateMap<UserForRegistrationDto, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
				.ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Name));


			CreateMap<Ride, RideDto>().ForMember(x => x.Orders, opt => opt.Ignore());
			CreateMap<User, UserDto>().ForMember(userDto => userDto.Rides, opt => opt.MapFrom(ride => ride.Rides));

			CreateMap<User, UserDto>().AfterMap((user, userDto, context) => userDto.Rides = context.Mapper.Map<List<RideDto>>(user.Rides))
				.ReverseMap().ForPath(user => user.Rides, opt => opt.Ignore());
			*/


			// Model => Dto & Dto => Model
			CreateMap<LocalityDto, Locality>().ReverseMap();
			CreateMap<RouteLocalityDto, RouteLocality>();
			CreateMap<CreateUpdateRouteDto, Route>();

			// Partial mapping, you need to make additional DB queries to gather required info.
			CreateMap<Route, RouteDto>().ForMember(routeDto => routeDto.SeatsAvailable, opt => opt.MapFrom(route =>
				Enumerable.Range(route.SeatStartNumber, route.SeatEndNumber - route.SeatStartNumber + 1).ToList()));


			CreateMap<Route, GetRouteDto>();
			CreateMap<RouteLocality, RouteLocalityDto>();
			CreateMap<Locality, LocalityDto>();



			// Dto => Dto
		}
	}
}
