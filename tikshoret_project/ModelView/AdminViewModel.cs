using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tikshoret_project.Models;

namespace tikshoret_project.ModelView
{
    public class AdminViewModel
    {
        public AdminBAL loginadmin { get; set; }
        public HallBAL halls { get; set; }
        public MoviesLibBAL movieslib { get; set; }

    }
}