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
    public class EnquiryDetailServices : IEnquiryDetailServices
    {
        private readonly IMapper mapper;
        private readonly IEnquiryDetailRepository enquiryDetailRepository;

        public EnquiryDetailServices(IMapper _mapper, IEnquiryDetailRepository _enquiryDetailRepository)
        {
            mapper = _mapper;
            enquiryDetailRepository = _enquiryDetailRepository;
        }

        public async Task<EnquiryDetailDTO> CreateEnquiryDetailAsync(CreateEnquiryDetailDTO createEnquiryDetailDTO)
        {
            try
            {
                var enquiryDetail = new EnquiryDetail()
                {
                    EnquiryId = createEnquiryDetailDTO.EnquiryId,
                    Note = createEnquiryDetailDTO.Note,
                    CreatedBy = createEnquiryDetailDTO.CreatedBy,
                    UpdatedBy = createEnquiryDetailDTO.CreatedBy,
                    CreatedDate = DateTimeOffset.Now,
                    UpdatedDate = DateTimeOffset.Now,
                    FollowUpDate = createEnquiryDetailDTO.FollowUpDate ?? createEnquiryDetailDTO.FollowUpDate
                };
                var newEnquiryDetail = await enquiryDetailRepository.CreateAsync(enquiryDetail);
                var _newEnquiryDetail = await enquiryDetailRepository.FindByIdAsync(newEnquiryDetail.EnquiryDetailsId);
                var mappedEnquiryDetail = enquiryDetailRepository.MapEnquiryDetails(_newEnquiryDetail);
                return mappedEnquiryDetail;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEnquiryDetailAsync(int id)
        {
            try
            {
                var oldEnquiryDetails = await enquiryDetailRepository.FindByIdAsync(id);
                if (oldEnquiryDetails != null)
                {
                    var isDeleted = await enquiryDetailRepository.DeleteAsync(oldEnquiryDetails);
                    return isDeleted;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EnquiryDetailDTO>> GetAllEnquiryDetailsAsync()
        {
            try
            {
                var enquiryDetails = await enquiryDetailRepository.FindAllAsync();
                var mappedEnquiryDetails = enquiryDetails.Select(x => enquiryDetailRepository.MapEnquiryDetails(x));
                return mappedEnquiryDetails;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EnquiryDetailDTO> GetEnquiryDetailByIdAsync(int id)
        {
            try
            {
                var enquiryDetails = await enquiryDetailRepository.FindByIdAsync(id);
                var mappedEnquiryDetail = enquiryDetailRepository.MapEnquiryDetails(enquiryDetails);
                return mappedEnquiryDetail;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EnquiryDetailDTO> UpdateEnquiryDetailAsync(int id, UpdateEnquiryDetailDTO updateEnquiryDetailDTO)
        {
            try
            {
                var oldEnquiryDetails = await enquiryDetailRepository.FindByIdAsync(id);
                if (oldEnquiryDetails != null)
                {
                    var enquiryDetail = new EnquiryDetail()
                    {
                        EnquiryId = oldEnquiryDetails.EnquiryId,
                        EnquiryDetailsId = updateEnquiryDetailDTO.EnquiryDetailsId,
                        Note = updateEnquiryDetailDTO.Note == oldEnquiryDetails.Note ? oldEnquiryDetails.Note : updateEnquiryDetailDTO.Note,
                        FollowUpDate = updateEnquiryDetailDTO.FollowUpDate ?? oldEnquiryDetails.FollowUpDate,
                        UpdatedBy = updateEnquiryDetailDTO.UpdatedBy,
                        CreatedBy = oldEnquiryDetails.CreatedBy,
                        CreatedDate = oldEnquiryDetails.CreatedDate,
                    };
                    var updatedEnquiryDetail = await enquiryDetailRepository.UpdateAsync(enquiryDetail);
                    var _updatedEnquiryDetail = await enquiryDetailRepository.FindByIdAsync(updatedEnquiryDetail.EnquiryDetailsId);
                    var mappedEnquiry = enquiryDetailRepository.MapEnquiryDetails(_updatedEnquiryDetail);
                    return mappedEnquiry;
                }
                else
                {
                    throw new InvalidOperationException($"Enquiry details with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
