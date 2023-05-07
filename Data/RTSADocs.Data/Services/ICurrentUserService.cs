using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Data.Services
{
    public interface ICurrentUserService
    {
        string GetUserName();
        string GetUserId();
    }
}
