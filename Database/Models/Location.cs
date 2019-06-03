using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Navn")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Vej")]
        public string Street { get; set; }
        [Required]
        [DisplayName("VejNummer")]
        public string StreetNumber { get; set; }
        [Required]
        [DisplayName("PostNummer")]
        public int ZipCode { get; set; }
        [Required]
        [DisplayName("By")]
        public string City { get; set; }


        public List<TreeTypeToMeasure> TreeTypesToMeasure { get; set; }
        public List<Sensor> Sensors { get; set; }

    }
}
