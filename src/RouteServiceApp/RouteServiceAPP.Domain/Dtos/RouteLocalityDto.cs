using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceAPP.Domain.Dtos
{
	public class RouteLocalityDto
	{
		//public int RouteId { get; set; }
		public int OrdinalNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }

		public LocalityDto Locality { get; set; }
	}
}
