using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.DTO
{
    public record CreateUserDTO(
        string UserName,
        string Phone,
        string Email,
        string Password,
        string Address,
        bool IsActive,
        int CreatedBy,
        int GenderId,
        int RoleId
    );
    public record UpdateUserDTO(
        int UserId,
        string UserName,
        string Phone,
        string Email,
        string Password,
        string Address,
        bool IsActive,
        int GenderId,
        int RoleId,
        int UpdatedBy
    );
    public record UserDTO(
        string UserName,
        string Phone,
        string Email,
        string Address,
        string Gender,
        string Role,
        DateTimeOffset CreatedDate
    );
}