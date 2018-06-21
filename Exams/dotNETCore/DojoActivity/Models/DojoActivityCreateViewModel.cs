using System;
using System.ComponentModel.DataAnnotations;

namespace beltexam.Models
{
    public class DojoActivityCreateViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title{get; set;}


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date {get; set;}

        [Required]
        [Display(Name = "Time")]
        public string Time {get; set;}

        [Required]
        [Display(Name = "Duration")]
        public string Duration {get; set;}
        
        [Required]
        public string DurationText {get; set;}

        [Required]
        [Display(Name = "Description")]
        public string Description {get; set;}
    }
}