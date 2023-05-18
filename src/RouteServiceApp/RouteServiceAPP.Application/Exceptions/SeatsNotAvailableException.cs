using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class SeatsNotAvailableException : RouteServiceAppException
	{
		public IEnumerable<int> Seats { get; set; }
		public SeatsNotAvailableException() : base("Selected seats are not available.", (int)HttpStatusCode.BadRequest)
		{
		}

		public SeatsNotAvailableException(string message) : base(message, (int)HttpStatusCode.BadRequest)
		{
		}

		public SeatsNotAvailableException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.BadRequest)
		{
		}

		public SeatsNotAvailableException(IEnumerable<int> seats) :
			base($"Selected seats are not available.\nSeats: {string.Join(", ", seats)}.", (int)HttpStatusCode.BadRequest)
		{
			Seats = seats;
		}
	}
}
