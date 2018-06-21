using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beltexam.Models
{
    public class DojoActivity
    {
        public int DojoActivityId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public string Title {get; set;}
        public string Duration {get; set;}
        public string DurationText {get; set;}
        public string Description {get; set;}
        public DateTime Date {get; set;}
        public string Time {get; set;}
        public DateTime CreatedAt {get; set;}
        public List<Participant> Participants {get; set;}

         public DojoActivity()
        {
            Participants = new List<Participant>();
        }
    }
}