using RouteServiceAPP.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouteServiceAPP.Application.Services.Interfaces
{
	public interface ILocalityService
	{
		Task<IEnumerable<LocalityDto>> GetLocalities();
		Task<LocalityDto> CreateLocality(string localityName);
		Task UpdateLocality(string localityName, string newLocalityName);
		Task DeleteLocality(string localityName);
	}
}
