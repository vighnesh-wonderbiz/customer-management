using CustomerManagement.Data;
using CustomerManagement.DTO;
using CustomerManagement.IRepositories;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{

    public class EnquiryDetailRepository : Repository<EnquiryDetail>, IEnquiryDetailRepository
    {
        private readonly CustomerManagementDbContext _context;
        public EnquiryDetailRepository(CustomerManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public EnquiryDetailDTO MapEnquiryDetails(EnquiryDetail enquiryDetail)
        {
            var enquiry = new EnquiryDTO(
                    enquiryDetail.EnquiryDetailsOfEnquiry.EnquiryOfStatus.StatusName,
                    enquiryDetail.EnquiryDetailsOfEnquiry.CreatedDate,
                    enquiryDetail.EnquiryDetailsOfEnquiry.EnquiryEmail,
                    enquiryDetail.EnquiryDetailsOfEnquiry.EnquiryName,
                    enquiryDetail.EnquiryDetailsOfEnquiry.EnquiryPhone,
                    enquiryDetail.EnquiryDetailsOfEnquiry.Source
            );
            var mappedEnquiry = new EnquiryDetailDTO(
            enquiry,
                enquiryDetail.FollowUpDate,
                enquiryDetail.Note,
                enquiryDetail.CreatedDate
            );
            return mappedEnquiry;

        }

        public async override Task<EnquiryDetail> FindByIdAsync(int id)
        {
            try
            {
                var enquiry = await _context.EnquiryDetails.Include(e => e.EnquiryDetailsOfEnquiry).ThenInclude(s => s.EnquiryOfStatus).FirstOrDefaultAsync(e => e.EnquiryDetailsId == id);
                if (enquiry != null)
                {
                    return enquiry;
                }
                throw new Exception($"No enquiry with id:{id}");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async override Task<IEnumerable<EnquiryDetail>> FindAllAsync()
        {
            try
            {
                var enquiries = await _context.EnquiryDetails.Include(e => e.EnquiryDetailsOfEnquiry).ThenInclude(s => s.EnquiryOfStatus).ToListAsync();
                return enquiries;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
