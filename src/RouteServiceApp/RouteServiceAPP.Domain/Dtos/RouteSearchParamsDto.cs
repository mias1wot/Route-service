using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceAPP.Domain.Dtos
{
	public class RouteSearchParamsDto
	{
		public string From { get; set; }
		public string To { get; set; }
		public DateTime DepartureTime { get; set; }
		public int NumberOfSeats { get; set; }
	}
}
