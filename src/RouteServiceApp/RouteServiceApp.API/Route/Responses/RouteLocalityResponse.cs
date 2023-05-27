using RouteServiceApp.API.Locality.Responses;
using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceApp.API.Route.Responses
{
	public class RouteLocalityResponse
	{
		//public int RouteId { get; set; }
		public int OrdinalNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }

		public LocalityResponse Locality { get; set; }
	}
}
