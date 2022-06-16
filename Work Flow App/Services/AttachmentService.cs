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
    public class AttachmentService : IAttachmentService
    {
        public readonly ApplicationDbContext _dbContext;

        public AttachmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Attachment>> GetAsync()
        {
            return await _dbContext.Attachments.ToListAsync();
        }

        public async Task<Attachment> GetByIdAsync(Guid id)
        {
            return await _dbContext.Attachments.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAsync(Attachment attachment)
        {
            await _dbContext.Attachments.AddAsync(attachment);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<bool> UpdateAsync(Attachment attachmentToUpdate)
        {
            _dbContext.Attachments.Update(attachmentToUpdate);
            var updated = await _dbContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var attachmentToDelete = await GetByIdAsync(id);

            if (attachmentToDelete == null)
                return false;

            _dbContext.Attachments.Remove(attachmentToDelete);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteByRequestIdAsync(int requestId)
        {
            var attachmentsToDelete = await GetByRequestIdAsync(requestId);

            if (attachmentsToDelete == null)
                return false;

            _dbContext.Attachments.RemoveRange(attachmentsToDelete);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Attachment>> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.Attachments.Where(x => x.RequestId == requestId).ToListAsync();
        }
    }
}
