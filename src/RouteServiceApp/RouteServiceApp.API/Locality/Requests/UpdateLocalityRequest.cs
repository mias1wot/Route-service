using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceApp.API.Locality.Requests
{
	public class UpdateLocalityRequest
	{
		public string Name { get; set; } = string.Empty;
		public string NewName { get; set; } = string.Empty;
	}
}
