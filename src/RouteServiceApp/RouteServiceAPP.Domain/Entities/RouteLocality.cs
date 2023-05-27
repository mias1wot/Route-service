using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceAPP.Domain.Entities
{
	public class RouteLocality
	{
		[Key]
		public int RouteId { get; set; }
		[Key]
		public int OrdinalNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }

		[Required]
		[ForeignKey(nameof(RouteId))]
		public Route Route { get; set; }
		[Required]
		public Locality Locality { get; set; }
	}
}
