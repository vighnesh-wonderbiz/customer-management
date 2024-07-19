using CustomerManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.IServices
{
    public interface IEnquiryDetailServices
    {

        Task<IEnumerable<EnquiryDetailDTO>> GetAllEnquiryDetailsAsync();

        Task<EnquiryDetailDTO> GetEnquiryDetailByIdAsync(int id);

        Task<bool> DeleteEnquiryDetailAsync(int id);

        Task<EnquiryDetailDTO> UpdateEnquiryDetailAsync(int id, UpdateEnquiryDetailDTO updateEnquiryDetailDTO);

        Task<EnquiryDetailDTO> CreateEnquiryDetailAsync(CreateEnquiryDetailDTO createEnquiryDetailDTO);
    }
}
