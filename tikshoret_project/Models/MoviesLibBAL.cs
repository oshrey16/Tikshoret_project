using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tikshoret_project.Models
{
    public class MoviesLibBAL
    {
        [Key]
        public string Movie_Id { get; set; }
        public string Genre { get; set; }
        public string Age { get; set; }
        [Required]
        public string Movie_Name { get; set; }
        public string Starring { get; set; }
        public byte[] Picture { get; set; }

    }
}