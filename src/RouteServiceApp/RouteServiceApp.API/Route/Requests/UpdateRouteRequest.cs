using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceApp.API.Route.Requests
{
	public class UpdateRouteRequest
	{
		public int RouteId { get; set; }
		public string ExtraInfo { get; set; }
		public int SeatStartNumber { get; set; }
		public int SeatEndNumber { get; set; } // Including the last number
		public ICollection<RouteLocalityRequest> RouteLocalities { get; set; } = new HashSet<RouteLocalityRequest>();
	}
}
