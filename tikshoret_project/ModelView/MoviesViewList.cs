using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tikshoret_project.Models;

namespace tikshoret_project.ModelView
{
    public class MoviesViewList
    {
        public List<MoviesLibBAL> movies { get; set; }
        public MovieShowBal movie { get; set; }


        public string SelectedShowId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public string SelectedHallId { get; set; }
        public IEnumerable<SelectListItem> ItemsHall { get; set; }

        public string SelectedTime { get; set; }
        public IEnumerable<SelectListItem> ItemsTime { get; set; }

        public string selectedDate { get; set; }

        public string selectedhr { get; set; }
        public IEnumerable<SelectListItem> Itemshr { get; set; }

        public List<int> seats { get; set; }
    }
}