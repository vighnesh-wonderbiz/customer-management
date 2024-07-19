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

    public class EnquiryRepository : Repository<Enquiry>, IEnquiryRepository
    {
        private readonly CustomerManagementDbContext _context;
        public EnquiryRepository(CustomerManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public EnquiryDTO MapEnquiries(Enquiry enquiry)
        {
            var mappedEnquiry = new EnquiryDTO(
                enquiry.EnquiryOfStatus.StatusName,
                enquiry.CreatedDate,
                enquiry.EnquiryEmail,
                enquiry.EnquiryName,
                enquiry.EnquiryPhone,
                enquiry.Source
            );
            return mappedEnquiry;

        }

        public async override Task<Enquiry> FindByIdAsync(int id)
        {
            try
            {
                var enquiry = await _context.Enquiries.Include(s => s.EnquiryOfStatus).FirstOrDefaultAsync(e => e.EnquiryId == id);
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

        public async override Task<IEnumerable<Enquiry>> FindAllAsync()
        {
            try
            {
                var enquiries = await _context.Enquiries.Include(s => s.EnquiryOfStatus).ToListAsync();
                return enquiries;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
