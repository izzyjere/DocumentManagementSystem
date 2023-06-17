using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Shared.Models
{
    public class DocumentArchive : AuditableEntity
    {
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
        public int Status { get; set; }
    }
}
