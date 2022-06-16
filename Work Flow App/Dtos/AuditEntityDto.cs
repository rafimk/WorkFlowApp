using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Dtos
{
    public class AuditEntityDto : EntityDto
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
