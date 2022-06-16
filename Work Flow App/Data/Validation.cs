using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Data
{
    public class Validation
    {
        public class Request
        {
            public const int MaxTitleLength = 254;
            public const int MaxDescriptionLength = 2000;
            public const int MaxNotesLength = 2000;
        }

        public class RequestWork
        {
            public const int MaxReasonLength = 2000;
        }
    }
}
