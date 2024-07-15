using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Model
{
    [Table("Enquiries")]
    public class Enquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryId { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status {  get; set; }

        public string EnquiryName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EnquiryEmail { get; set; } = string.Empty;

        [MaxLength(10, ErrorMessage = "Invalid Phone")]
        public string EnquiryPhone { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual ICollection<EnquiryInterest> EnquiryInterests { get; set; }
        public virtual ICollection<EnquiryDetail> EnquiryDetails { get; set; }
    }
}
