using Microsoft.EntityFrameworkCore;
using Pri.Pe1.Timo.Blomme.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Pe1.Timo.Blomme.Web.Data.Seeding
{
    public class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            Event[] events = new Event[]
            {
                new Event { Id = 1, Title = "Hackathon", Date = new DateTime(2025, 10, 20), Location = "Gent", MaxParticipants = 50 },
                new Event { Id = 2, Title = "CodeCamp", Date = new DateTime(2025, 11, 5), Location = "Brugge", MaxParticipants = 100 },
                new Event { Id = 3, Title = "TechNight", Date = new DateTime(2025, 12, 1), Location = "Antwerpen", MaxParticipants = 75 }
            };

            Participant[] participants = new Participant[]
            {
                new Participant { Id = 1, Name = "Tom Jansen", Email = "tom@example.com", EventId = 1 },
                new Participant { Id = 2, Name = "Lisa Peeters", Email = "lisa@example.com", EventId = 2 },
                new Participant { Id = 3, Name = "Jan De Smet", Email = "jan@example.com", EventId = 3 }
            };

            modelBuilder.Entity<Event>().HasData(events);
            modelBuilder.Entity<Participant>().HasData(participants);
        }
    }
}
