using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserName();

        int GetId();
    }
}
