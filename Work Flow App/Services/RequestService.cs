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
    public class RequestService : IRequestService
    {
        public readonly ApplicationDbContext _dbContext;

        public RequestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Request>> GetAsync()
        {
            return await _dbContext.Requests.ToListAsync();
        }

        public async Task<List<Request>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Requests.Where(x => x.CreatedBy == userId)
                                                  .Include(x => x.User)
                                                  .OrderByDescending(x => x.CreatedOn)
                                                  .ToListAsync();
        }

        public async Task<List<Request>> GetForAdminAsync()
        {
            return await _dbContext.Requests.Where(x => x.Status == 2 ||
                                                        x.Status == 4 ||
                                                        x.Status == 5)
                                                  .Include(x => x.User)
                                                  .OrderByDescending(x => x.CreatedOn)
                                                  .ToListAsync();
        }

        public async Task<Request> GetByIdAsync(int requestId)
        {
            return await _dbContext.Requests.Include(x => x.Attachments).SingleOrDefaultAsync(x => x.Id == requestId);
        }

        public async Task<int> CreateAsync(Request request)
        {
            request.Id = 0;
            request.Status = 1;
            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return request.Id;
        }
        public async Task<bool> UpdateAsync(Request requestToUpdate)
        {
            _dbContext.Requests.Update(requestToUpdate);
            var updated = await _dbContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteAsync(int requestId)
        {
            var requestToDelete = await GetByIdAsync(requestId);

            if (requestToDelete == null)
                return false;

            _dbContext.Requests.Remove(requestToDelete);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ChanveApprvalStatusAsync(int requestId, string action)
        {
            int status = 0;
            switch (action.ToLower())
            {
                case "modified":
                    status = 1;
                    break;
                case "submitted":
                    status = 2;
                    break;
                case "approved":
                    status = 5;
                    break;
                case "rejected":
                    status = 4;
                    break;
                case "returned":
                    status = 3;
                    break;
                default:
                    break;
            }
            
            var requestFromDb = await GetByIdAsync(requestId);
            if (requestFromDb == null)
            {
                return false;
            }
            requestFromDb.Status = status;
            _dbContext.Requests.Update(requestFromDb);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
