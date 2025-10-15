using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; } = default!;

        [Range(1, 500)]
        public int MaxParticipants { get; set; }

        public ICollection<Participant>? Participants { get; set; }
    }
}
