using FluentValidation;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Route
{
	public class BookRideParamsDtoValidator : AbstractValidator<BookRideParamsDto>
	{
		public BookRideParamsDtoValidator()
		{
			RuleFor(req => req.RouteId).NotNull().NotEmpty();
			RuleFor(req => req.From).NotNull().NotEmpty();
			RuleFor(req => req.To).NotNull().NotEmpty();
			RuleFor(req => req.Seats).NotNull().NotEmpty();
		}
	}
}
