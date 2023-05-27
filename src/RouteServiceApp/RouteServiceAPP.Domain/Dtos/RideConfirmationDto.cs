using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceAPP.Domain.Dtos
{
	public class RideConfirmationDto
	{
		public bool IsSuccess { get; set; }
		public IEnumerable<string> Errors { get; set; }
		public int RouteId { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public string From { get; set; } = string.Empty;
		public string To { get; set; } = string.Empty;
		public IEnumerable<SeatDto> Seats { get; set; }
		public string ExtraInfo { get; set; }
	}
}
