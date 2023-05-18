using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceAPP.Domain.Dtos
{
	public class RouteDto
	{
		public int RouteId { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public string From { get; set; } = string.Empty;
		public string To { get; set; } = string.Empty;
		public IEnumerable<int> SeatsAvailable { get; set; } // Numbers of available Seats
		public string ExtraInfo { get; set; }
	}
}
