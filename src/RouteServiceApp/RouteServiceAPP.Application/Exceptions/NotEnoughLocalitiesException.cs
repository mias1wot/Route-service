using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class NotEnoughLocalitiesException : RouteServiceAppException
	{
		public NotEnoughLocalitiesException() : base("You need to provide more localities.", (int)HttpStatusCode.BadRequest)
		{
		}

		public NotEnoughLocalitiesException(string message) : base(message, (int)HttpStatusCode.BadRequest)
		{
		}

		public NotEnoughLocalitiesException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.BadRequest)
		{
		}

		public NotEnoughLocalitiesException(int minLocalitiesRequired) :
			base($"You need to provide at least {minLocalitiesRequired} localities.", (int)HttpStatusCode.BadRequest)
		{
		}

		public NotEnoughLocalitiesException(int minLocalitiesRequired, int localitiesGiven) :
			base($"You need to provide at least {minLocalitiesRequired} localities. Given {localitiesGiven} localities.", (int)HttpStatusCode.BadRequest)
		{
		}
	}
}
