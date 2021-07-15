using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tikshoret_project.Models;

namespace tikshoret_project.ModelView
{
    public class AccountViewModel
    {
        public AccountBAL account { get; set; }
        public List<int> seats { get; set; }

        public List<TicketsBal> tickets { get; set; }
        public List<PaidTicketsBal> PaidTickets { get; set; }
    }
}