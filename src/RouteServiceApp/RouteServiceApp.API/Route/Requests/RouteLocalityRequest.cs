using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RouteServiceApp.API.Route.Requests
{
	public class RouteLocalityRequest
	{
		public int OrdinalNumber { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }

		public LocalityRequest Locality { get; set; }
	}
}
