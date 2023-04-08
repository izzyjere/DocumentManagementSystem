using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSADocs.Shared.DTOs
{
    public class TokenResponse
    {
        public DateTime Expires { get; set; }
        public string Data { get; set; }
    }
}
