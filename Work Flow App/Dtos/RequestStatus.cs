using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Dtos
{
    public enum RequestStatus
    {
        Draft = 1,
        Pending = 2,
        Returned = 3,
        Rejected = 4,
        Approved = 5
    }
}
