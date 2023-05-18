using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceAPP.Domain.Entities
{
	// FK is a PK for the fastest search - a clustered index will be built on BookedRouteId,
	// so that when we search all Seats of a BookedRoute, it'll be done quickly.
	public class Seat
	{
		[Key]
		public int BookedRouteId { get; set; }
		[Key]
		public int Number { get; set; }

		[Required]
		[ForeignKey(nameof(BookedRouteId))]
		public BookedRoute BookedRoute { get; set; }
	}
}
