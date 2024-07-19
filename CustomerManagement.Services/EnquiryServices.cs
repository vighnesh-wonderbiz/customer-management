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
    public class EnquiryServices : IEnquiryServices
    {
        private readonly IMapper mapper;
        private readonly IEnquiryRepository enquiryRepository;

        public EnquiryServices(IMapper _mapper, IEnquiryRepository _enquiryRepository)
        {
            mapper = _mapper;
            enquiryRepository = _enquiryRepository;
        }

        public async Task<EnquiryDTO> CreateEnquiryAsync(CreateEnquiryDTO createEnquiryDTO)
        {
            try
            {
                var enquiry = mapper.Map<Enquiry>(createEnquiryDTO);
                enquiry.CreatedDate = DateTimeOffset.Now;
                enquiry.UpdatedDate = DateTimeOffset.Now;
                enquiry.UpdatedBy = createEnquiryDTO.CreatedBy;
                var newEnquiry = await enquiryRepository.CreateAsync(enquiry);
                var _newEnquiry = await enquiryRepository.FindByIdAsync(newEnquiry.EnquiryId);
                var mappedEnquiry = enquiryRepository.MapEnquiries(_newEnquiry);
                return mappedEnquiry;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEnquiryAsync(int id)
        {
            try
            {
                var oldEnquiry = await enquiryRepository.FindByIdAsync(id);
                if (oldEnquiry != null)
                {
                    var isDeleted = await enquiryRepository.DeleteAsync(oldEnquiry);
                    return isDeleted;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EnquiryDTO>> GetAllEnquiriesAsync()
        {
            try
            {
                var enquiries = await enquiryRepository.FindAllAsync();
                var mappedEnquiry = enquiries.Select(x => enquiryRepository.MapEnquiries(x));
                return mappedEnquiry;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EnquiryDTO> GetEnquiryByIdAsync(int id)
        {
            try
            {
                var enquiry = await enquiryRepository.FindByIdAsync(id);
                var mappedEnquiry = enquiryRepository.MapEnquiries(enquiry);
                return mappedEnquiry;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EnquiryDTO> UpdateEnquiryAsync(int id, UpdateEnquiryDTO updateEnquiryDTO)
        {
            try
            {
                var oldEnquiry = await enquiryRepository.FindByIdAsync(id);
                if (oldEnquiry != null)
                {
                    var enquiry = new Enquiry()
                    {
                        EnquiryId = updateEnquiryDTO.EnquiryId,
                        StatusId = updateEnquiryDTO.StatusId == oldEnquiry.StatusId ? oldEnquiry.StatusId : updateEnquiryDTO.StatusId,
                        UpdatedBy = updateEnquiryDTO.UpdatedBy,
                        EnquiryEmail = updateEnquiryDTO.EnquiryEmail ?? oldEnquiry.EnquiryEmail,
                        EnquiryPhone = updateEnquiryDTO.EnquiryPhone ?? oldEnquiry.EnquiryPhone,
                        EnquiryName = updateEnquiryDTO.EnquiryName ?? oldEnquiry.EnquiryName,
                        Source = updateEnquiryDTO.Source ?? oldEnquiry.Source
                    };
                    var updatedEnquiry = await enquiryRepository.UpdateAsync(enquiry);
                    var _updatedEnquiry = await enquiryRepository.FindByIdAsync(updatedEnquiry.EnquiryId);
                    var mappedEnquiry = enquiryRepository.MapEnquiries(_updatedEnquiry);
                    return mappedEnquiry;
                }
                else
                {
                    throw new InvalidOperationException($"Enquiry with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
