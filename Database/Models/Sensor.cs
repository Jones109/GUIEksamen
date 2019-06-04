using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Sensor
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Trætype")]
        public string TreeType { get; set; }

        [Required]
        [DisplayName("Sensor Id")]
        [MinLength(16)]
        [MaxLength(16)]
        [RegularExpression("[0-9a-fA-F]+",
            ErrorMessage = "Skal være hexadecimal")]
        [Key]
        public string SensorId { get; set; }
        [Required]
        [DisplayName("Latitude")]
        public double GpsLat { get; set; }
        [Required]
        [DisplayName("Longitude ")]
        public double GpsLon { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
