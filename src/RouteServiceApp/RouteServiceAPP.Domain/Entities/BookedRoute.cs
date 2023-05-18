using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceAPP.Domain.Entities
{
	public class BookedRoute
	{
		public int BookedRouteId { get; set; }

		[Required]
		public virtual Route Route { get; set; }
		[Required]
		public Locality From { get; set; }
		[Required]
		public Locality To { get; set; }
		public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
	}
}
