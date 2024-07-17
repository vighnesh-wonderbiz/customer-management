using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Model
{
    [Table("PaymentRecords")]
    public class PaymentRecord
    {
        public int PaymentRecordId {get;set;}

        [Precision(18, 2)]
        public decimal Amount {get;set;}

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public virtual Payment PaymentRecordOfPayment { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        
    }
}
