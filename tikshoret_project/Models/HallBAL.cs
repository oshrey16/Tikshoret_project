using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tikshoret_project.Models
{
    public class HallBAL
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Hall_type { get; set; }
        [Required]
        public int Seats_count { get; set; }
    }
}