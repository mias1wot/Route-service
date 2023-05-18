using FluentValidation;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Route
{
	public class RouteSearchParamsDtoValidator : AbstractValidator<RouteSearchParamsDto>
	{
		public RouteSearchParamsDtoValidator()
		{
			RuleFor(req => req.From).NotNull().NotEmpty();
			RuleFor(req => req.To).NotNull().NotEmpty();
			RuleFor(req => req.DepartureTime).NotNull().NotEmpty().Must(date => date > DateTime.Now).WithMessage("Departure date is incorrect.");
			RuleFor(req => req.NumberOfSeats).GreaterThan(0);
		}
	}
}
