using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.core.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
