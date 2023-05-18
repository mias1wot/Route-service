using FluentValidation;
using RouteServiceApp.API.Route.Requests;
using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Locality
{
	public class LocalityRequestValidator : AbstractValidator<LocalityRequest>
	{
		public LocalityRequestValidator()
		{
			RuleFor(req => req.Name).NotNull().NotEmpty().MaximumLength(100);
		}
	}
}
