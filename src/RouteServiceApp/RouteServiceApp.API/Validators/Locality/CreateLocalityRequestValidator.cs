using FluentValidation;
using RouteServiceApp.API.Locality.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Validators.Locality
{
	//FluentValidation.AspNetCore (v. 9.2)
	public class CreateLocalityRequestValidator : AbstractValidator<CreateLocalityRequest>
	{
		public CreateLocalityRequestValidator()
		{
			RuleFor(req => req.Name).NotNull().NotEmpty().MaximumLength(100);
		}
	}
}
