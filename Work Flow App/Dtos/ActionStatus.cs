using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Dtos
{
    public enum ActionStatus
    {
        Created = 1,
        Modified = 2,
        Submitted = 3,
        Returned = 4,
        Rejected = 5,
        Approved = 6
    }
}
