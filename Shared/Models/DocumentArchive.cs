using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Shared.Models
{
    public class DocumentArchive : AuditableEntity
    {
        public Guid DocumentId { get; set; } // ID of the associated document
        public Document Document { get; set; } // Associated document object
        public int Status { get; set; } // Status code representing the status of the document archive
    }
}
