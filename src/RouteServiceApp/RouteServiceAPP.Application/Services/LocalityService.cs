using AutoMapper;
using RouteServiceAPP.Application.Exceptions;
using RouteServiceAPP.Application.Services.Interfaces;
using RouteServiceAPP.Domain.Dtos;
using RouteServiceAPP.Domain.Entities;
using RouteServiceAPP.Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteServiceAPP.Application
{
	public class LocalityService: ILocalityService
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
		public LocalityService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<IEnumerable<LocalityDto>> GetLocalities()
		{
			return _mapper.Map<List<LocalityDto>>(await _unitOfWork.LocalityRepo.GetAsync());
		}

		public async Task<LocalityDto> CreateLocality(string localityName)
		{

			Locality createdLocality = await _unitOfWork.LocalityRepo.CreateAsync(new Locality { Name = localityName });
			await _unitOfWork.SaveAsync();

			return _mapper.Map<LocalityDto>(createdLocality);
		}

		public async Task UpdateLocality(string localityName, string newLocalityName)
		{
			Locality locality = await _unitOfWork.LocalityRepo.GetAsync(localityName);
			if(locality is null)
			{
				throw new LocalityNotFoundException(localityName);
			}

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await DeleteLocality(localityName);
				await CreateLocality(newLocalityName);

				await _unitOfWork.CommitTransactionAsync();
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw;
			}
		}

		public async Task DeleteLocality(string localityName)
		{
			Locality locality = await _unitOfWork.LocalityRepo.GetAsync(localityName);
			if (locality is null)
			{
				throw new LocalityNotFoundException(localityName);
			}

			await _unitOfWork.LocalityRepo.DeleteAsync(localityName);
			await _unitOfWork.SaveAsync();
		}
	}
}
