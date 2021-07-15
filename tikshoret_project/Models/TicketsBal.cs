using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tikshoret_project.Models
{
    public class TicketsBal
    {
        
        [Key]
        [Required]
        public int Ticket_index { get; set; }

        [ForeignKey("Movies")]
        [Required]
        public int movie_index { get; set; }
        public virtual MovieShowBal Movies { get; set; }


        [ForeignKey("Accounts")]
        [Required]
        public string cid { get; set; }
        public virtual AccountBAL Accounts { get; set; }

        [Required]
        public string tcol { get; set; }
        [Required]
        public string trow { get; set; }
    }
}