using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Models.RequestModels
{
    public class ParticipantCreateRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int EventId { get; set; }
    }
}
