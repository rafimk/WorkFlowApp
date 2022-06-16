using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;

namespace Work_Flow_App.Services.Interfaces
{
    public interface IRequestWorkFlowService
    {
        Task<bool> CreatedAsync(int requestId, int userId);
        Task<bool> ModifiedAsync(int requestId, int userId);

        Task<bool> SendForApprovalAsync(int requestId, int userId,string reason);

        Task<bool> AdminActionAsync(int requestId, int userId, string reason, string action);

        Task<List<RequestWorkFlow>> GetByRequestIdAsync(int requestId);
    }
}
