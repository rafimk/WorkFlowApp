using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Models;

namespace Work_Flow_App.Dtos
{
    public class AttachmentDto : FileModel
    {
        public int RequestId { get; set; }
        public RequestDto Request { get; set; }
    }
}
