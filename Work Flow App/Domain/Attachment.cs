using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Work_Flow_App.Domain
{
    public class Attachment : FileModel
    {
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
