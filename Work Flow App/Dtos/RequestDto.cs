using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using static Work_Flow_App.Data.Validation.Request;

namespace Work_Flow_App.Dtos
{
    public class RequestDto : AuditEntityDto
    {
        [MaxLength(MaxTitleLength)]
        [Required(ErrorMessage = "Please Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter Details")]
        [MaxLength(MaxDescriptionLength)]
        public string Details { get; set; }
        [MaxLength(MaxNotesLength)]
        public string Notes { get; set; }
        public int Status { get; set; }
        public User User { get; set; }

        public List<AttachmentDto> Attachments { get; set; }
        public List<RequestWorkFlowDto> RequestWorkFlows { get; set; }

        public DateTime CreatedOnLoc
        {
            get
            {
                return CreatedOn.ToLocalTime();
            }
        }

        public RequestStatus RequestStatus
        {
            get
            {
                return (RequestStatus)Status;
            }
        }
    }
}
