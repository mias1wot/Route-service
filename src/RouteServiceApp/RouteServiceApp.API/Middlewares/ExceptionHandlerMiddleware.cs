using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RouteServiceApp.API.Middlewares
{
	// This is used if someone forgets to use Exception filter.
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				response.StatusCode = (int)HttpStatusCode.InternalServerError;


				string responseText = e.Message;
				if (e.InnerException != null)
				{
					responseText += $"\nInner exception:\n{e.InnerException.Message}";
				}

				await response.WriteAsync(JsonSerializer.Serialize(new { Value = responseText }));
			}
		}
	}
}
