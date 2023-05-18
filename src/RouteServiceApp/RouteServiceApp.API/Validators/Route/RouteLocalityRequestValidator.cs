using FluentValidation;
using RouteServiceApp.API.Route.Requests;
using RouteServiceApp.API.Validators.Locality;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Route
{
	public class RouteLocalityRequestValidator : AbstractValidator<RouteLocalityRequest>
	{
		public RouteLocalityRequestValidator()
		{
			RuleFor(req => req.OrdinalNumber).NotNull().NotEmpty();
			RuleFor(req => req.DepartureTime).NotNull().NotEmpty();
			RuleFor(req => req.ArrivalTime).NotNull().NotEmpty().Must(date => date > DateTime.Now).WithMessage("Departure date is incorrect.");
			RuleFor(req => req.Locality).SetValidator(new LocalityRequestValidator());
		}
	}
}
