using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Work_Flow_App.Data.Validation.Request;

namespace Work_Flow_App.Domain
{
    public class Request : AuditEntity
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Details { get; set; }
        [MaxLength(MaxNotesLength)]
        public string Notes { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<RequestWorkFlow> RequestWorkFlows { get; set; }
    }
}
