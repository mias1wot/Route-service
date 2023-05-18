using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class RouteNotFoundException : RouteServiceAppException
	{
		public RouteNotFoundException() : base("Route not found.", (int)HttpStatusCode.NotFound)
		{
		}

		public RouteNotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
		{
		}

		public RouteNotFoundException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.NotFound)
		{
		}

		public RouteNotFoundException(object routeId) : base($"Route with id {routeId} not found.", (int)HttpStatusCode.NotFound)
		{
		}
	}
}
