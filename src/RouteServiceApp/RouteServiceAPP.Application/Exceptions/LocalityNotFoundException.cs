using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class LocalityNotFoundException : RouteServiceAppException
	{
		public LocalityNotFoundException() : base("Locality not found.", (int)HttpStatusCode.NotFound)
		{
		}

		public LocalityNotFoundException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.NotFound)
		{
		}

		public LocalityNotFoundException(string localityName) :
			base($"Locality '{localityName}' not found.", (int)HttpStatusCode.NotFound)
		{
		}
	}
}
