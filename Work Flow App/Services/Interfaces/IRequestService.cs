using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;

namespace Work_Flow_App.Services.Interfaces
{
    public interface IRequestService
    {
        Task<List<Request>> GetAsync();
        Task<List<Request>> GetByUserIdAsync(int userId);
        Task<List<Request>> GetForAdminAsync();
        Task<Request> GetByIdAsync(int requestId);
        Task<int> CreateAsync(Request request);
        Task<bool> UpdateAsync(Request requestToUpdate);
        Task<bool> DeleteAsync(int requestId);
        Task<bool> ChanveApprvalStatusAsync(int requestId, string action);
    }
}
