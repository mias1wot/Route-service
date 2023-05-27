using FluentValidation;
using RouteServiceApp.API.Locality.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Locality
{
	public class UpdateLocalityRequestValidator : AbstractValidator<UpdateLocalityRequest>
	{
		public UpdateLocalityRequestValidator()
		{
			RuleFor(req => req.Name).NotNull().NotEmpty().MaximumLength(100);
			RuleFor(req => req.NewName).NotNull().NotEmpty().MaximumLength(100);
		}
	}
}
