using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tikshoret_project.Models
{
    public class MovieShowBal
    {
        [Key]
        [Required]
        public int Movie_index { get; set; }

        [Required]
        [ForeignKey("MoviesLibBAL")]
        public string Movie_Id { get; set; }
        public virtual MoviesLibBAL MoviesLibBAL { get; set; }

        [Required(ErrorMessage = "Please select Hall")]
        [ForeignKey("HallBAL")]
        public int Hall_Id { get; set; }
        public virtual HallBAL HallBAL { get; set; }

        [Required(ErrorMessage = "Please select Date&Time")]
        public DateTime Movie_Time { get; set; }

        [Required]
        public int price { get; set; }
    }
}