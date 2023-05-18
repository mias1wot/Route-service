using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteServiceApp.API.Filters;
using RouteServiceApp.API.Route.Requests;
using RouteServiceApp.API.Route.Responses;
using RouteServiceAPP.Application.Services.Interfaces;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[ApiExceptionFilter]
	public class RouteController : ControllerBase
	{
		readonly IRouteService _routeService;
		readonly IMapper _mapper;
		readonly IValidator<CreateRouteRequest> _createRouteRequestValidator;//, IValidator<> Validator
		readonly IValidator<UpdateRouteRequest> _updateRouteRequestValidator;
		readonly IValidator<RouteSearchParamsDto> _routeSearchParamsDtoValidator;
		readonly IValidator<BookRideParamsDto> _bookRideParamsDtoValidator;
		public RouteController(IRouteService routeService, IMapper mapper,
			IValidator<CreateRouteRequest> createRouteRequestValidator, IValidator<UpdateRouteRequest> updateRouteRequestValidator, IValidator<RouteSearchParamsDto> routeSearchParamsDtoValidator, IValidator<BookRideParamsDto> bookRideParamsDtoValidator)
		{
			_routeService = routeService;
			_mapper = mapper;

			_createRouteRequestValidator = createRouteRequestValidator;
			_updateRouteRequestValidator = updateRouteRequestValidator;
			_routeSearchParamsDtoValidator = routeSearchParamsDtoValidator;
			_bookRideParamsDtoValidator = bookRideParamsDtoValidator;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetRouteResponse>>> GetAllRoutes()
		{
			return Ok(_mapper.Map<List<GetRouteResponse>>(await _routeService.GetAllRoutes()));
		}

		[HttpPost]
		public async Task<ActionResult<RouteResponse>> CreateRoute(CreateRouteRequest request)
		{
			var validationRes = await _createRouteRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			RouteDto routeDto = await _routeService.CreateRoute(_mapper.Map<CreateUpdateRouteDto>(request));

			return Ok(_mapper.Map<RouteResponse>(routeDto));
		}

		[HttpPut]
		public async Task<ActionResult> UpdateRoute(UpdateRouteRequest request)
		{
			var validationRes = await _updateRouteRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			await _routeService.UpdateRoute(_mapper.Map<CreateUpdateRouteDto>(request));

			return NoContent();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteRoute(int routeId)
		{
			await _routeService.DeleteRoute(routeId);

			return NoContent();
		}



		[HttpPost]
		public async Task<ActionResult<IEnumerable<RouteDto>>> GetAvailableRoutesAsync(RouteSearchParamsDto request)
		{
			var validationRes = await _routeSearchParamsDtoValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			return Ok(await _routeService.GetAvailableRoutesAsync(request));
		}

		[HttpPost]
		public async Task<ActionResult<RideConfirmationDto>> BookRideAsync(BookRideParamsDto request)
		{
			var validationRes = await _bookRideParamsDtoValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			return Ok(await _routeService.BookRideAsync(request));
		}
	}
}
