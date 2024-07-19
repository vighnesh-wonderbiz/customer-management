using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.DTO
{
    public record StatusDTO(
        string StatusName,
        bool IsActive,
        DateTimeOffset? CreatedDate
    );

    public record CreateStatusDTO(
        string StatusName,
        bool IsActive,
        int CreatedBy
    );

    public record UpdateStatusDTO(
        int StatusId,
        string StatusName,
        bool IsActive,
        int UpdatedBy
    );
}