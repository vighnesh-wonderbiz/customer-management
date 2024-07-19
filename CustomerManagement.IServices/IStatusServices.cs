using CustomerManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.IServices
{
    public interface IStatusServices
    {
        Task<IEnumerable<StatusDTO>> GetAllStatusesAsync();

        Task<StatusDTO> GetStatusByIdAsync(int id);

        Task<bool> DeleteStatusAsync(int id);

        Task<StatusDTO> UpdateStatusAsync(int id, UpdateStatusDTO updateStatusDTO);

        Task<StatusDTO> CreateStatusAsync(CreateStatusDTO createStatusDTO);
    }
}
