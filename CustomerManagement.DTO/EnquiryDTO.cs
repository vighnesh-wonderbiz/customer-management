using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.DTO
{
    public record EnquiryDTO(
        string Status,
        DateTimeOffset CreatedDate,
        string? EnquiryEmail,
        string? EnquiryName,
        string? EnquiryPhone,
        string? Source
    );


    public record CreateEnquiryDTO(
        int CreatedBy,
        string? EnquiryEmail,
        string? EnquiryName,
        string? EnquiryPhone,
        string? Source,
        int? StatusId
    );

    public record UpdateEnquiryDTO(
        int EnquiryId,
        int StatusId,
        int UpdatedBy,
        string? EnquiryEmail,
        string? EnquiryName,
        string? EnquiryPhone,
        string? Source
    );
}