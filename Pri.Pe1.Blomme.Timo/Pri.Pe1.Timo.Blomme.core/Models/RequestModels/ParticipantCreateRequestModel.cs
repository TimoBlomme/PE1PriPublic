using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Models.RequestModels
{
    public class ParticipantCreateRequestModel
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int EventId { get; set; }
    }
}
