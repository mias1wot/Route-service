using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class TheSameLocalityException : RouteServiceAppException
	{
		public TheSameLocalityException() : base("Start locality cannot be the same as the destination one.", (int)HttpStatusCode.BadRequest)
		{
		}

		public TheSameLocalityException(string message) : base(message, (int)HttpStatusCode.BadRequest)
		{
		}

		public TheSameLocalityException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.BadRequest)
		{
		}

		public TheSameLocalityException(string startLocalityName, string destinationLocalityName) :
			base($"Start locality cannot be the same as the destination one.\nStart locality: '{startLocalityName}'\nDestination locality:'{destinationLocalityName}'.", (int)HttpStatusCode.BadRequest)
		{
		}
	}
}
