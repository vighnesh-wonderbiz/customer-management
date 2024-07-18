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
    public record OrderDTO(
        decimal OrderTotal,
        decimal Discount,
        decimal Balance,
        DateTimeOffset? BalanceReminder,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        UserDTO OrderOfUser,
        IEnumerable<OrderDetailDTO> CurrentOrderDetails,
        DateTimeOffset CreatedDate
        // IEnumerable<Payment> PaymentsOfOrder
    );

    public record CreatedOrderDTO(
        decimal OrderTotal,
        decimal Discount,
        decimal Balance,
        DateTimeOffset? BalanceReminder,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        IEnumerable<OrderDetailDTO> CurrentOrderDetails,
        DateTimeOffset CreatedDate
    );

    public record CreateOrderDTO(
        decimal Discount,
        decimal Balance,
        DateTimeOffset? BalanceReminder,
        int UserId,
        int CreatedBy,
        int UpdatedBy,
        IEnumerable<CreateOrderDetailDTO> CurrentOrderDetails
    );
    public record UpdateOrderDTO(
        int OrderId,
        decimal OrderTotal,
        decimal Discount,
        decimal Balance,
        DateTimeOffset? BalanceReminder,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        int UserId,
        DateTimeOffset CreatedDate,
        DateTimeOffset UpdatedDate,
        int CreatedBy,
        int UpdatedBy
    );
    
}