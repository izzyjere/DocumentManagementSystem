using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Shared.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
