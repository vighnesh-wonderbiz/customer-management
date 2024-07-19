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
    public record OrderDetailDTO(
        int Quantity,
        int ProductId,
        decimal ProductPrice,
        decimal ProdcutSubtotal, // productPrice * quantity
        decimal ProductTotal, // ProductSubtotal + Gst
        decimal Discount,
        decimal IGst,
        decimal CGst,
        decimal SGst,
        decimal UTGst,
        ProductOrderDetail OrderDetailOfProduct,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        DateTimeOffset CreatedDate
    );

    public record CreateOrderDetailDTO(
        int Quantity,
        decimal Discount,
        int ProductId,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        DateTimeOffset CreatedDate,
        DateTimeOffset UpdatedDate,
        int CreatedBy,
        int UpdatedBy
    );

    public record UpdateOrderDetailDTO(
        int OrderDetailsId,
        int Quantity,
        decimal Discount,
        decimal IGst,
        decimal CGst,
        decimal SGst,
        decimal UTGst,
        int ProductId,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        DateTimeOffset UpdatedDate,
        int UpdatedBy
    );
}