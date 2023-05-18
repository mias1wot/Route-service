using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Application.Services.Interfaces
{
	public interface IRouteService
	{
		Task<IEnumerable<GetRouteDto>> GetAllRoutes();
		Task<RouteDto> CreateRoute(CreateUpdateRouteDto createRouteDto);
		Task UpdateRoute(CreateUpdateRouteDto updateRouteDto);
		Task DeleteRoute(int routeId);

		Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto);
		Task<RideConfirmationDto> BookRideAsync(BookRideParamsDto bookRideParamsDto);
	}
}
