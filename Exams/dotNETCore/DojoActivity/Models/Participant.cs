using System;
using System.Collections.Generic;

namespace beltexam.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public int UserParticipantId { get; set; }
        public User UserParticipant { get; set; }
        public int EventId { get; set; }
        public DojoActivity Event { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}