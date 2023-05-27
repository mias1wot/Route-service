using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class SeatsNotFoundException : RouteServiceAppException
	{
		public IEnumerable<int> Seats { get; set; }
		public SeatsNotFoundException() : base("Selected seats don't exist for the chosen route.", (int)HttpStatusCode.NotFound)
		{
		}

		public SeatsNotFoundException(string message) : base(message, (int)HttpStatusCode.NotFound)
		{
		}

		public SeatsNotFoundException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.NotFound)
		{
		}

		public SeatsNotFoundException(IEnumerable<int> seats) :
			base($"Selected seats don't exist for the chosen route.\nSeats: {string.Join(", ", seats)}.", (int)HttpStatusCode.NotFound)
		{
			Seats = seats;
		}
	}
}
