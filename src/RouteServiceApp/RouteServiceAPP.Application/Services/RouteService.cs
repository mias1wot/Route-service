using AutoMapper;
using RouteServiceAPP.Application.Exceptions;
using RouteServiceAPP.Application.Services.Interfaces;
using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using RouteServiceAPP.Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceAPP.Application
{
	public class RouteService: IRouteService
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
		public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<IEnumerable<GetRouteDto>> GetAllRoutes()
		{
			var routes = await _unitOfWork.RouteRepo.GetRoutesWithLocalitiesAsync();

			return _mapper.Map<List<GetRouteDto>>(routes);
		}

		public async Task<RouteDto> CreateRoute(CreateUpdateRouteDto createRouteDto)
		{
			Route route = _mapper.Map<Route>(createRouteDto);


			if (createRouteDto.RouteLocalities.Count() < 2)
			{
				throw new NotEnoughLocalitiesException(2, createRouteDto.RouteLocalities.Count());
			}


			foreach (RouteLocality routeLocality in route.RouteLocalities)
			{
				var curLocality = await _unitOfWork.LocalityRepo.GetAsync(routeLocality.Locality.Name);
				if (curLocality is null)
				{
					throw new LocalityNotFoundException(routeLocality.Locality.Name);
				}

				routeLocality.Locality = curLocality;
			}

			

			Route createdRoute = await _unitOfWork.RouteRepo.CreateAsync(route);
			await _unitOfWork.SaveAsync();

			// Load RouteLocalities.
			_unitOfWork.RouteRepo.LoadNavigationProperty(createdRoute, nameof(Route.RouteLocalities));

			RouteDto routeDto = _mapper.Map<RouteDto>(createdRoute);

			var routeLocalities = createdRoute.RouteLocalities.OrderBy(routeLoc => routeLoc.OrdinalNumber);

			RouteLocality startRouteLocality = routeLocalities.First();
			RouteLocality destinationRouteLocality = routeLocalities.Last();

			// Load Locality of RouteLocalities.
			_unitOfWork.RouteLocalityRepo.LoadNavigationProperty(startRouteLocality, nameof(RouteLocality.Locality));
			_unitOfWork.RouteLocalityRepo.LoadNavigationProperty(destinationRouteLocality, nameof(RouteLocality.Locality));

			routeDto.DepartureTime = startRouteLocality.DepartureTime;
			routeDto.From = startRouteLocality.Locality.Name;

			routeDto.ArrivalTime = destinationRouteLocality.ArrivalTime;
			routeDto.To = destinationRouteLocality.Locality.Name;

			return routeDto;
		}

		public async Task UpdateRoute(CreateUpdateRouteDto updateRouteDto)
		{
			if (updateRouteDto.RouteLocalities.Count() < 2)
			{
				throw new NotEnoughLocalitiesException(2, updateRouteDto.RouteLocalities.Count());
			}


			Route route = await _unitOfWork.RouteRepo.GetRouteWithRouteLocalitiesAsync(updateRouteDto.RouteId);
			if(route is null)
			{
				throw new RouteNotFoundException(updateRouteDto.RouteId);
			}


			_mapper.Map(updateRouteDto, route);


			foreach (RouteLocality routeLocality in route.RouteLocalities)
			{
				var curLocality = await _unitOfWork.LocalityRepo.GetAsync(routeLocality.Locality.Name);
				if (curLocality is null)
				{
					throw new LocalityNotFoundException(routeLocality.Locality.Name);
				}

				routeLocality.Locality = curLocality;
			}
			

			await _unitOfWork.RouteRepo.UpdateAsync(route);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteRoute(int routeId)
		{
			Route route = await _unitOfWork.RouteRepo.GetAsync(routeId);
			if (route is null)
			{
				throw new RouteNotFoundException(routeId);	
			}

			await _unitOfWork.RouteRepo.DeleteAsync(routeId);
			await _unitOfWork.SaveAsync();
		}


		public async Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto)
		{
			var fromLocality = await _unitOfWork.LocalityRepo.GetAsync(routeSearchParamsDto.From);
			if (fromLocality is null)
			{
				throw new LocalityNotFoundException(routeSearchParamsDto.From);
			}

			var toLocality = await _unitOfWork.LocalityRepo.GetAsync(routeSearchParamsDto.To);
			if (toLocality is null)
			{
				throw new LocalityNotFoundException(routeSearchParamsDto.To);
			}


			//var availableRoutes = new List<(Route route, List<int> availableSeats)>();
			var availableRoutes = new List<RouteDto>();


			IEnumerable<Route> routes = await _unitOfWork.RouteRepo.GetRoutesAsync();

			string from = routeSearchParamsDto.From;
			string to = routeSearchParamsDto.To;
			foreach(Route route in routes)
			{
				// Builds the route by all intermediate localities. The localities go in a real order.
				var routeLocalities = route.RouteLocalities.OrderBy(routeLocality => routeLocality.OrdinalNumber).Select(routeLocality => routeLocality.Locality.Name);

				if (routeLocalities.Contains(from) && routeLocalities.Contains(to))
				{
					// This route goes through needed localities.


					// Check if time is OK
					DateTime departureTime = route.RouteLocalities.Where(routeLoc => routeLoc.Locality.Name == from).First().DepartureTime;
					if (departureTime < routeSearchParamsDto.DepartureTime ||
						departureTime > routeSearchParamsDto.DepartureTime.Date.AddDays(1)) // to the end of the day
					{
						// Time is not convenient - skip this route
						continue;	
					}


					// Check if there are enough seats available
					IEnumerable<int> seatsAvailable = GetAvailableSeats(route, routeLocalities, from, to);


					// Add to results if enough available seats
					if (seatsAvailable.Count() >= routeSearchParamsDto.NumberOfSeats)
					{
						availableRoutes.Add(new RouteDto
						{
							RouteId = route.RouteId,
							DepartureTime = departureTime,
							ArrivalTime = route.RouteLocalities.Where(routeLoc => routeLoc.Locality.Name == to).First().ArrivalTime,
							From = route.RouteLocalities.First().Locality.Name,
							To = route.RouteLocalities.Last().Locality.Name,
							SeatsAvailable = seatsAvailable,
							ExtraInfo = route.ExtraInfo
						});
					}
				}
			}

			return availableRoutes;
		}

		public async Task<RideConfirmationDto> BookRideAsync(BookRideParamsDto bookRideParamsDto)
		{
			var route = await _unitOfWork.RouteRepo.GetRouteWithLocalitiesAndBoookedRoutesAsync(bookRideParamsDto.RouteId);
			if(route is null)
			{
				//throw new RouteNotFoundException(bookRideParamsDto.RouteId);
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Route with id {bookRideParamsDto.RouteId} not found." }
				};
			}

			// Builds the route by all intermediate localities. The localities go in a real order.
			var routeLocalities = route.RouteLocalities.OrderBy(routeLocality => routeLocality.OrdinalNumber)
				.Select(routeLocality => routeLocality.Locality).ToList();


			var fromLocalityInd = routeLocalities.FindIndex(routeLoc => routeLoc.Name == bookRideParamsDto.From);
			if (fromLocalityInd == -1)
			{
				//throw new LocalityNotFoundException(bookRideParamsDto.From, LocalityPositions.Start);
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Start locality '{bookRideParamsDto.From}' not found." }
				};
			}

			var toLocalityInd = routeLocalities.FindIndex(routeLoc => routeLoc.Name == bookRideParamsDto.To);
			if (toLocalityInd == -1)
			{
				//throw new LocalityNotFoundException(bookRideParamsDto.From, LocalityPositions.Destination);
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Destination locality '{bookRideParamsDto.To}' not found." }
				};
			}

			if(fromLocalityInd >= toLocalityInd)
			{
				//throw new TheSameLocalityException(bookRideParamsDto.From, bookRideParamsDto.To);
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Start locality cannot be the same as the destination one.\nStart locality: '{bookRideParamsDto.From}'\nDestination locality:'{bookRideParamsDto.To}'." }
				};
			}

			var fromLocality = routeLocalities[fromLocalityInd];
			var toLocality = routeLocalities[toLocalityInd];


			var notFoundSeats = bookRideParamsDto.Seats.Where(seatNumber => seatNumber < route.SeatStartNumber || seatNumber > route.SeatEndNumber);
			if (notFoundSeats.Any())
			{
				//throw new SeatsNotFoundException(notFoundSeats.ToList());
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Selected seats don't exist for the chosen route.\nSeats: {string.Join(", ", notFoundSeats.ToList())}." }
				};
			}

			// Check if given seats are available
			IEnumerable<int> seatsAvailable = GetAvailableSeats(route, routeLocalities.Select(routeLoc => routeLoc.Name), bookRideParamsDto.From, bookRideParamsDto.To);

			var seatsNotAvailable = bookRideParamsDto.Seats.Where(requiredSeat => !seatsAvailable.Contains(requiredSeat));

			if (seatsNotAvailable.Any())
			{
				//throw new SeatsNotAvailableException(seatsNotAvailable.ToList());
				return new RideConfirmationDto
				{
					Errors = new List<string> { $"Selected seats are not available.\nSeats: {string.Join(", ", seatsNotAvailable.ToList())}." }
				};
			}


			ICollection<Seat> seatsToBook = bookRideParamsDto.Seats.Select(seatNumber => new Seat { Number = seatNumber }).ToList();

			BookedRoute bookedRoute = new BookedRoute
			{
				Route = route,
				From = fromLocality,
				To = toLocality,
				Seats = seatsToBook
			};
			await _unitOfWork.BookedRouteRepo.CreateAsync(bookedRoute);
			await _unitOfWork.SaveAsync();


			var departureTime = route.RouteLocalities.Where(routeLoc => routeLoc.Locality.Name == fromLocality.Name).First().DepartureTime;
			var arrivalTime = route.RouteLocalities.Where(routeLoc => routeLoc.Locality.Name == toLocality.Name).First().ArrivalTime;
			return new RideConfirmationDto
			{
				IsSuccess = true,
				RouteId = route.RouteId,
				DepartureTime = departureTime,
				ArrivalTime = arrivalTime,
				From = fromLocality.Name,
				To = toLocality.Name,
				Seats = bookedRoute.Seats.Select(bookedRouteSeat => new SeatDto
				{
					Number = bookedRouteSeat.Number
				}),
				ExtraInfo = route.ExtraInfo
			};
		}


		// Returns numbers of available seats
		private IEnumerable<int> GetAvailableSeats(Route route, IEnumerable<string> routeLocalities, string fromLocalityName, string toLocalityName)
		{
			// Get all route seats - from route.SeatStartNumber to route.SeatEndNumber (inclusive)
			List<int> seatsAvailable = Enumerable.Range(route.SeatStartNumber, route.SeatEndNumber - route.SeatStartNumber + 1).ToList();


			// Find all localities that a passenger will go through.
			var neededRouteLocalities = routeLocalities.SkipWhile(locality => locality != fromLocalityName)
				.TakeWhile(locality => locality != toLocalityName).ToList();
			neededRouteLocalities.Add(toLocalityName);

			foreach (BookedRoute bookedRoute in route.BookedRoutes)
			{
				// Find all localities that a passenger with that booked route will go through.
				var bookedRouteLocalities = routeLocalities.SkipWhile(locality => locality != bookedRoute.From.Name)
					.TakeWhile(locality => locality != bookedRoute.To.Name).ToList();
				bookedRouteLocalities.Add(bookedRoute.To.Name);


				// Check if booked route intersects the needed route. Complexity is O(4*n) operations.
				bool routeIsbooked = false;
				// If needed route is a part of a booked route
				if (bookedRouteLocalities.Contains(fromLocalityName) && bookedRouteLocalities.Last() != fromLocalityName)
				{
					routeIsbooked = true;

				}
				if (bookedRouteLocalities.Contains(toLocalityName) && bookedRouteLocalities.First() != toLocalityName)
				{
					routeIsbooked = true;
				}

				// If booked route is a part of a needed route
				if (neededRouteLocalities.Contains(bookedRoute.From.Name) && neededRouteLocalities.Last() != bookedRoute.From.Name)
				{
					routeIsbooked = true;

				}
				if (neededRouteLocalities.Contains(bookedRoute.To.Name) && neededRouteLocalities.First() != bookedRoute.To.Name)
				{
					routeIsbooked = true;
				}


				if (routeIsbooked)
				{
					// Remove unavailable Seats.
					bookedRoute.Seats.Select(seat => seat.Number).ToList()
						.ForEach(seatNumber => seatsAvailable.Remove(seatNumber));
				}
			}


			return seatsAvailable;
		}
	}
}
