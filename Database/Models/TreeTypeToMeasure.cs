using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class TreeTypeToMeasure
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Antal")]
        public int Count { get; set; }
        [Required]
        [DisplayName("Trætype")]
        public string TreeType { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }

    }
}
