using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tikshoret_project.Models
{
    public class PaidTicketsBal
    {

        [Key]
        [ForeignKey("Tickets")]
        public int Ticket_index { get; set; }
        public virtual TicketsBal Tickets { get; set; }
    }
}