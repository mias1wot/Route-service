using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RouteServiceAPP.Domain.Entities
{
	//[Index(nameof(Locality.Name), IsUnique = true)] // It'd be useful if Name wouldn't be PK
	public class Locality
	{
		[Key]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;
	}
}
