using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RouteServiceAPP.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Filters
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

		public ApiExceptionFilterAttribute()
		{
			_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{typeof(RouteServiceAppException), HandleRouteServiceAppException},
			};
		}

		public override void OnException(ExceptionContext context)
		{
			HandleException(context);

			base.OnException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			Type type = context.Exception.GetType();

			// Try get the same type as the exception one
			if (_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext> action))
			{
				action.Invoke(context);
				return;
			}


			// Try get the parent type of the exception one
			foreach (var exceptionHandler in _exceptionHandlers)
			{
				if (exceptionHandler.Key.IsAssignableFrom(type))
				{
					exceptionHandler.Value.Invoke(context);
					return;
				}
			}
		}

		private void HandleRouteServiceAppException(ExceptionContext context)
		{
			var ex = context.Exception as RouteServiceAppException;

			context.Result = new ObjectResult(ex.Message)
			{
				StatusCode = ex.StatusCode
			};

			context.ExceptionHandled = true;
		}
	}
}
