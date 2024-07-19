using CustomerManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.IServices
{
    public interface IEnquiryServices
    {

        Task<IEnumerable<EnquiryDTO>> GetAllEnquiriesAsync();

        Task<EnquiryDTO> GetEnquiryByIdAsync(int id);

        Task<bool> DeleteEnquiryAsync(int id);

        Task<EnquiryDTO> UpdateEnquiryAsync(int id, UpdateEnquiryDTO updateEnquiryDTO);

        Task<EnquiryDTO> CreateEnquiryAsync(CreateEnquiryDTO createEnquiryDTO);
    }
}
