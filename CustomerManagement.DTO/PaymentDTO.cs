using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.DTO
{
    public record PaymentDTO(
        decimal Price,
        decimal Balance,
        DateTimeOffset CreatedDate,
        int CreatedBy,
        DateTimeOffset UpdatedDate,
        int UpdatedBy,
        List<PaymentRecord> PaymentRecordsId
    );

    public record CreatePaymentDTO(
        decimal Price,
        DateTimeOffset CreatedDate,
        int CreatedBy,
        DateTimeOffset UpdatedDate,
        int UpdatedBy,
        IEnumerable PaymentRecordsId

    );
/*
    public record UpdateProductDTO(
        bool IsSubscribable,
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        bool IsActive,
        decimal IGstRate,
        decimal CGstRate,
        decimal UTGstRate,
        decimal SGstRate,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        int CreatedBy,
        DateTimeOffset UpdatedDate,
        int UpdatedBy,
        int ProductId,
        int DurationInDays = 0
    );
    */
}