using FluentValidation;
using RouteServiceApp.API.Route.Requests;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Route
{
	public class UpdateRouteRequestValidator : AbstractValidator<UpdateRouteRequest>
	{
		public UpdateRouteRequestValidator()
		{
			RuleFor(req => req.RouteId).NotNull().NotEmpty();
			RuleFor(req => req.ExtraInfo).NotNull().NotEmpty().MaximumLength(300);
			RuleFor(req => req.SeatStartNumber).NotNull().NotEmpty();
			RuleFor(req => req.SeatEndNumber).NotNull().NotEmpty().Must((req, seatEndNumber) => seatEndNumber >= req.SeatStartNumber).WithMessage("SeatEndNumber must be greater than or equal to SeatStartNumber.");
			RuleFor(req => req.RouteLocalities).NotNull().NotEmpty();
			RuleForEach(req => req.RouteLocalities).SetValidator(new RouteLocalityRequestValidator());
		}
	}
}
