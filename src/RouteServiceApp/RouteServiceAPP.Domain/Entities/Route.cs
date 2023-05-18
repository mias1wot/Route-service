using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceAPP.Domain.Entities
{
	public class Route
	{
		public int RouteId { get; set; }
		[StringLength(300)]
		public string ExtraInfo { get; set; }
		public int SeatStartNumber { get; set; }
		public int SeatEndNumber { get; set; } // Including the last number

		public ICollection<RouteLocality> RouteLocalities { get; set; } = new HashSet<RouteLocality>();
		public ICollection<BookedRoute> BookedRoutes { get; set; } = new HashSet<BookedRoute>();
	}
}
