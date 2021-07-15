using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tikshoret_project.Models;

namespace tikshoret_project.ModelView
{
    public class TicketsViewModel
    {
        public TicketsBal ticket { get; set; }

        public List<TicketsBal> tickets { get; set; }

        public List<seatBal> soldseats { get; set; }
        public List<string> rseats { get; set; }
        public List<string> cseats { get; set; }

        public string selected_active { get; set; }
        public List<string> rseats_selected { get; set; }
        public List<string> cseats_selected { get; set; }
    }
}