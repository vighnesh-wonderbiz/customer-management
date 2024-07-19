using AutoMapper;
using CustomerManagement.DTO;
using CustomerManagement.IRepositories;
using CustomerManagement.IServices;
using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public class StatusServices : IStatusServices
    {
        private readonly IStatusRepository statusRepository;

        public StatusServices(IStatusRepository _statusRepository)
        {
            statusRepository = _statusRepository;
        }

        public async Task<StatusDTO> CreateStatusAsync(CreateStatusDTO createStatusDTO)
        {
            try
            {
                var status = new Status()
                {
                    StatusName = createStatusDTO.StatusName,
                    IsActive = createStatusDTO.IsActive,
                    CreatedDate = DateTimeOffset.Now,
                    UpdatedDate = DateTimeOffset.Now,
                    CreatedBy = createStatusDTO.CreatedBy,
                    UpdatedBy = createStatusDTO.CreatedBy
                };
                var newStatus = await statusRepository.CreateAsync(status);
                var mappedStatus = new StatusDTO(newStatus.StatusName, newStatus.IsActive, newStatus.CreatedDate);
                return mappedStatus;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteStatusAsync(int id)
        {
            try
            {
                var oldStatus = await statusRepository.FindByIdAsync(id);
                if (oldStatus != null)
                {
                    var isDeleted = await statusRepository.DeleteAsync(oldStatus);
                    return isDeleted;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<StatusDTO>> GetAllStatusesAsync()
        {
            try
            {
                var statuses = await statusRepository.FindAllAsync();
                var mappedStatuses = statuses.Select(x => new StatusDTO(x.StatusName, x.IsActive, x.CreatedDate));
                return mappedStatuses;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<StatusDTO> GetStatusByIdAsync(int id)
        {
            try
            {
                var status = await statusRepository.FindByIdAsync(id);
                if (status != null)
                {
                    var mappedStatus = new StatusDTO(status.StatusName, status.IsActive, status.CreatedDate);
                    return mappedStatus;
                }
                throw new Exception($"No Status with Id: {id}");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<StatusDTO> UpdateStatusAsync(int id, UpdateStatusDTO updateStatusDTO)
        {
            try
            {
                var oldStatus = await statusRepository.FindByIdAsync(id);
                if (oldStatus != null)
                {
                    oldStatus.StatusName = updateStatusDTO.StatusName;
                    oldStatus.StatusId = updateStatusDTO.StatusId;
                    oldStatus.IsActive = updateStatusDTO.IsActive;
                    oldStatus.UpdatedDate = DateTimeOffset.Now;
                    oldStatus.UpdatedBy = updateStatusDTO.UpdatedBy;
                    var updatedStatus = await statusRepository.UpdateAsync(oldStatus);
                    var mappedStatus = new StatusDTO(updatedStatus.StatusName, updatedStatus.IsActive, updatedStatus.CreatedDate);
                    return mappedStatus;
                }
                throw new Exception($"No Status with id:{id}");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
