using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.DTO
{
    public record EnquiryDetailDTO(
        EnquiryDTO Enquiry,
        DateTimeOffset? FollowUpDate,
        string Note,
        DateTimeOffset CreatedDate
    );

    public record CreateEnquiryDetailDTO(
        int EnquiryId,
        string Note,
        DateTimeOffset? FollowUpDate,
        int CreatedBy
    );

    public record UpdateEnquiryDetailDTO(
        int EnquiryDetailsId,
        string Note,
        DateTimeOffset? FollowUpDate,
        int UpdatedBy
    );
}