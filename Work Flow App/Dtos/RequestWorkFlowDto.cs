using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using static Work_Flow_App.Data.Validation.RequestWork;

namespace Work_Flow_App.Dtos
{
    public class RequestWorkFlowDto
    {
        [Key]
        public int Id { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        [MaxLength(MaxReasonLength)]
        public string Reason { get; set; }
        public int Status { get; set; }
        public int RequestId { get; set; }

        public User User { get; set; }

        public DateTime ActionDateLoc
        {
            get
            {
                return ActionDate.ToLocalTime();
            }
        }

        public ActionStatus ActionStatus
        {
            get
            {
                return (ActionStatus)Status;
            }
        }
    }
}
