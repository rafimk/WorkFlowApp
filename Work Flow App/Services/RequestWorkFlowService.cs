using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Data;
using Work_Flow_App.Domain;
using Work_Flow_App.Services.Interfaces;

namespace Work_Flow_App.Services
{
    public class RequestWorkFlowService : IRequestWorkFlowService
    {
        public readonly ApplicationDbContext _dbContext;

        public RequestWorkFlowService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreatedAsync(int requestId, int userId)
        {
            var createRequestWorkFlow = new RequestWorkFlow
            {
                ActionBy = userId,
                Reason = "",
                ActionDate = DateTime.UtcNow,
                Status = 1,
                RequestId = requestId
            };
            await _dbContext.RequestWorkFlows.AddAsync(createRequestWorkFlow);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> ModifiedAsync(int requestId, int userId)
        {
            var createRequestWorkFlow = new RequestWorkFlow
            {
                ActionBy = userId,
                Reason = "",
                ActionDate = DateTime.UtcNow,
                Status = 2,
                RequestId = requestId
            };
            await _dbContext.RequestWorkFlows.AddAsync(createRequestWorkFlow);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> SendForApprovalAsync(int requestId, int userId, string reason)
        {
            var createRequestWorkFlow = new RequestWorkFlow
            {
                ActionBy = userId,
                Reason = reason,
                ActionDate = DateTime.UtcNow,
                Status = 3,
                RequestId = requestId
            };
            await _dbContext.RequestWorkFlows.AddAsync(createRequestWorkFlow);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> AdminActionAsync(int requestId, int userId, string reason, string action)
        {
            int status = 0;
            switch (action.ToLower())
            {
                case "approved":
                    status = 6;
                    break;
                case "rejected":
                    status = 5;
                    break;
                case "returned":
                    status = 4;
                    break;
                default:
                    break;
            }
            var createRequestWorkFlow = new RequestWorkFlow
            {
                ActionBy = userId,
                Reason = reason,
                ActionDate = DateTime.UtcNow,
                Status = status,
                RequestId = requestId
            };
            await _dbContext.RequestWorkFlows.AddAsync(createRequestWorkFlow);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<RequestWorkFlow>> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.RequestWorkFlows.Where(x => x.RequestId == requestId)
                                                    .Include(x => x.User)
                                                    .OrderByDescending(x => x.ActionDate)
                                                    .ToListAsync();
        }
    }
}
