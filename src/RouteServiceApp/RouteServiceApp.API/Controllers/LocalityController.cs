using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteServiceApp.API.Filters;
using RouteServiceApp.API.Locality.Requests;
using RouteServiceApp.API.Locality.Responses;
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
	public class LocalityController : ControllerBase
	{
		readonly ILocalityService _localityService;
		readonly IMapper _mapper;
		readonly IValidator<CreateLocalityRequest> _createLocalityRequestValidator;
		readonly IValidator<UpdateLocalityRequest> _updateLocalityRequestValidator;
		public LocalityController(ILocalityService localityService, IMapper mapper,
			IValidator<CreateLocalityRequest> createLocalityRequestValidator, IValidator<UpdateLocalityRequest> updateLocalityRequestValidator)
		{
			_localityService = localityService;
			_mapper = mapper;
			_createLocalityRequestValidator = createLocalityRequestValidator;
			_updateLocalityRequestValidator = updateLocalityRequestValidator;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<LocalityResponse>>> GetLocalities()
		{

			return Ok(_mapper.Map<List<LocalityResponse>>(await _localityService.GetLocalities()));
		}

		[HttpPost]
		public async Task<ActionResult<LocalityResponse>> CreateLocality(CreateLocalityRequest request)
		{
			var validationRes = await _createLocalityRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			return Ok(_mapper.Map<LocalityResponse>(await _localityService.CreateLocality(request.Name)));
		}

		[HttpPut]
		public async Task<ActionResult> UpdateLocality(UpdateLocalityRequest request)
		{
			var validationRes = await _updateLocalityRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}

			await _localityService.UpdateLocality(request.Name, request.NewName);

			return NoContent();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteLocality(string localityName)
		{
			await _localityService.DeleteLocality(localityName);

			return NoContent();
		}
	}
}
