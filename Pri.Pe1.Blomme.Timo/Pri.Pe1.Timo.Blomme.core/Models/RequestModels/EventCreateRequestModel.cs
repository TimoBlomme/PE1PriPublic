using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Models.RequestModels
{
    public class EventCreateRequestModel
    {
        public string Title { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = default!;
        public int MaxParticipants { get; set; }
    }
}
