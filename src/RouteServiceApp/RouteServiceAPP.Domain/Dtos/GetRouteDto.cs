using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceAPP.Domain.Dtos
{
	public class GetRouteDto
	{
		public int RouteId { get; set; }
		public string ExtraInfo { get; set; }
		public int SeatStartNumber { get; set; }
		public int SeatEndNumber { get; set; } // Including the last number
		public ICollection<RouteLocalityDto> RouteLocalities { get; set; } = new HashSet<RouteLocalityDto>();
	}
}
