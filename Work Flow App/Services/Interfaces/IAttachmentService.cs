using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;

namespace Work_Flow_App.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<List<Attachment>> GetAsync();
        Task<Attachment> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Attachment attachment);
        Task<bool> UpdateAsync(Attachment attachmentToUpdate);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteByRequestIdAsync(int requestId);
        Task<List<Attachment>> GetByRequestIdAsync(int requestId);
    }
}
