using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tikshoret_project.Models;
using tikshoret_project.Dal;
using tikshoret_project.ModelView;
using System.Globalization;

namespace tikshoret_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AccountDal dal = new AccountDal();
            MoviesViewList mv = new MoviesViewList();
            List<MoviesLibBAL> lmovies = dal.MoviesLib.ToList<MoviesLibBAL>();
            mv.movies = lmovies;
            return View(mv);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Movies()
        {
            AccountDal dal = new AccountDal();
            MoviesViewList mv = new MoviesViewList();
            mv.movies = dal.MoviesLib.ToList<MoviesLibBAL>();
            return View(mv);
        }

        public ActionResult Order()
        {
            return View();
        }

        public ActionResult AccountLogin()
        {
            if (Session["UserID"] == null)
                return View();


            return RedirectToAction("Account");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountLogin1()
        {
            if (ModelState.IsValid)
            {
                List<AccountBAL> account = new List<AccountBAL>();
                AccountDal dal = new AccountDal();
                string searchValueId = Request.Form["account.Id"].ToString();
                string searchValuePass = Request.Form["account.Pass"].ToString();
                AccountViewModel cvm = new AccountViewModel();
                account = (from x in dal.Accounts where x.Id == (searchValueId) && x.Pass == (searchValuePass) select x).ToList<AccountBAL>();

                if (account.Count == 1)
                {

                    cvm.account = account[0];
                    Session["UserName"] = cvm.account.Cname;
                    Session["UserID"] = cvm.account.Id.ToString();
                    Session["Msg"] = "Login Success!!";
                    Session["Navigation"] = "Account";
                    return RedirectToAction("MsgPage");
                }
                cvm.account = null;
                Session["Msg"] = "Fail Login";

                return View("AccountLogin");
            }
            return RedirectToAction("AccountLogin");
        }

        public ActionResult MsgPage(AccountViewModel cvm)
        {
            return View(cvm);
        }

        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["UserID"] = null;
            Session["Administrator"] = null;
            Session["Msg"] = "Log Out Success!";
            Session["Navigation"] = "Index";
            return RedirectToAction("MsgPage");
        }
        public ActionResult Register()
        {
            AccountViewModel cvm = new AccountViewModel();
            cvm.account = new AccountBAL();
            return View(cvm);
        }
        [HttpPost]
        public ActionResult Register(AccountViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                AccountBAL ac = new AccountBAL();
                ac.Id = cvm.account.Id;
                ac.Cname = cvm.account.Cname;
                ac.Caddress = cvm.account.Caddress;
                ac.bdate = cvm.account.bdate;
                ac.Phone = cvm.account.Phone;
                ac.Email = cvm.account.Email;
                ac.Pass = cvm.account.Pass;
                AccountDal dal = new AccountDal();
                List<AccountBAL> account = new List<AccountBAL>();
                account = (from x in dal.Accounts where x.Id == ac.Id select x).ToList<AccountBAL>(); ;
                if (account == null)
                {
                    dal.Accounts.Add(ac);
                    dal.SaveChanges();
                    Session["Msg"] = "Register Success!!";
                    Session["Navigation"] = "Account";
                    return RedirectToAction("MsgPage");
                }
                Session["Msg"] = "User Exists!!";
                Session["Navigation"] = "Register";
                return RedirectToAction("MsgPage");
            }
            return View(cvm);
        }

        public ActionResult Account()
        {
            if(Session["UserID"] != null)
            {
            AccountDal dal = new AccountDal();
            AccountViewModel mv = new AccountViewModel();
            List<MovieShowBal> movies = new List<MovieShowBal>();
            string cid = Session["UserID"].ToString();
            mv.PaidTickets = (from x in dal.PaidTickets where x.Tickets.cid == cid select x).ToList<PaidTicketsBal>();
            return View(mv);
            }
            Session["Msg"] = "Please Login!!";
            Session["Navigation"] = "AccountLogin";
            return RedirectToAction("MsgPage");
        }

        public ActionResult OrderMovieIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderMovieIndex(MoviesViewList mv)
        {
            if(Session["ticketsbagids"] == null)
            {
                Session["ticketsbagids"] = new List<int>();
            }
            AccountDal dal = new AccountDal();
            List<MoviesLibBAL> movies = new List<MoviesLibBAL>();
            string date = Request.Form["DateShow"].ToString();
            Session["SelectedDate"] = date;
            mv.selectedDate = date;
            string Movie_Time = DateTime.Parse(date).ToString("dd/MM/yyyy");
            List<string> moviesid = new List<string>();
            foreach (MovieShowBal movie in dal.Movies)
            {
                if (movie.Movie_Time.ToString("dd/MM/yyyy") == Movie_Time)
                {
                    moviesid.Add(movie.Movie_Id);
                }
            }
            moviesid = moviesid.Distinct().ToList();

            for (int i = 0; i < moviesid.Count(); i++)
            {
                foreach (MoviesLibBAL movie in dal.MoviesLib)
                {
                    if (movie.Movie_Id == moviesid[i])
                    {
                        movies.Add(movie);
                    }
                }
            }

            List<SelectListItem> movieslist = new List<SelectListItem>();
            foreach (MoviesLibBAL movie in movies)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = movie.Movie_Name,
                    Value = movie.Movie_Id,
                };
                movieslist.Add(selectList);
            }
            if (movieslist.Count == 0)
            {
                Session["Msg"] = "No Movies in this Date!!";
                Session["Navigation"] = "OrderMovieIndex";
                return RedirectToAction("MsgPage");
            }
            mv.Items = movieslist;
            return View("SelectMovieAfterDate", mv);
        }


        public ActionResult SelectMovieAfterDate()
        {
            Session["Msg"] = "Please try Again!";
            Session["Navigation"] = "OrderMovieIndex";
            return RedirectToAction("MsgPage");
        }

        [HttpPost]
        public ActionResult SelectMovieAfterDate(MoviesViewList mv)
        {
            Session["Selected_Movie_Id"] = mv.movie.Movie_Id;
            AccountDal dal = new AccountDal();
            string date = Session["SelectedDate"].ToString();
            string movieId = mv.movie.Movie_Id;
            List<HallBAL> halls = new List<HallBAL>();

            List<int> hallsid = new List<int>();
            foreach (MovieShowBal movie in dal.Movies)
            {
                if (movie.Movie_Time.ToString("dd/MM/yyyy") == date)
                {
                    if (movie.Movie_Id == movieId)
                    {
                        hallsid.Add(movie.Hall_Id);
                    }
                }
            }

            for (int i = 0; i < hallsid.Count(); i++)
            {
                foreach (HallBAL hall in dal.Halls)
                {
                    if (hall.ID == hallsid[i])
                        halls.Add(hall);
                }
            }

            List<SelectListItem> halllist = new List<SelectListItem>();
            foreach (HallBAL hall in halls)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = hall.ID.ToString() + ": " + hall.Hall_type,
                    Value = hall.ID.ToString()
                };
                halllist.Add(selectList);
            }
            mv.ItemsHall = halllist;
            return View("SelectHallAfterMovie", mv);
        }

        public ActionResult SelectHallAfterMovie()
        {
            Session["Msg"] = "Please try Again!";
            Session["Navigation"] = "OrderMovieIndex";
            return RedirectToAction("MsgPage");
        }


        [HttpPost]
        public ActionResult SelectHallAfterMovie(MoviesViewList mv)
        {
            AccountDal dal = new AccountDal();
            Session["Selected_Hall_Id"] = mv.movie.Hall_Id.ToString();
            List<HallBAL> hall = (from x in dal.Halls where x.ID == mv.movie.Hall_Id select x).ToList<HallBAL>();
            Session["Selected_Hall_Type"] = hall[0].Hall_type;
            Session["Selected_Hall_countseats"] = hall[0].Seats_count;
            string date = Session["SelectedDate"].ToString();
            string movieId = Session["Selected_Movie_Id"].ToString();
            string hallId = mv.movie.Hall_Id.ToString();
            List<HallBAL> halls = new List<HallBAL>();

            List<string> hours = new List<string>();
            List<SelectListItem> hourlist = new List<SelectListItem>();
            foreach (MovieShowBal movie in dal.Movies)
            {
                if (movie.Movie_Time.ToString("dd/MM/yyyy") == date)
                {
                    if (movie.Movie_Id == movieId)
                    {
                        if (movie.Hall_Id.ToString() == hallId)
                        {
                            string hr = movie.Movie_Time.ToString("HH:mm:ss");
                            SelectListItem selectList = new SelectListItem()
                            {
                                Text = hr,
                                Value = hr
                            };
                            hourlist.Add(selectList);
                        }
                    }
                }
            }
            mv.Itemshr = hourlist;
            return View("SelectHrAfterHall", mv);
        }


        public ActionResult SelectHrAfterHall()
        {
            Session["Msg"] = "Please try Again!";
            Session["Navigation"] = "OrderMovieIndex";
            return RedirectToAction("MsgPage");
        }

        [HttpPost]
        public ActionResult SelectHrAfterHall(MoviesViewList mv)
        {
            AccountDal dal = new AccountDal();
            mv.movie = new MovieShowBal();
            MovieShowBal smovie = new MovieShowBal();
            string hr = Request.Form["TimeList"].ToString();
            Session["Selected_hr"] = hr;
            string date = Session["SelectedDate"].ToString();
            string movieId = Session["Selected_Movie_Id"].ToString();
            string hallId = Session["Selected_Hall_Id"].ToString();
            var fulldate = date + " " + hr;

            foreach (MovieShowBal movie in dal.Movies)
            {
                if (movie.Movie_Time.ToString("dd/MM/yyyy HH:mm:ss") == fulldate)
                {
                    if (movie.Movie_Id == movieId)
                    {
                        if (movie.Hall_Id.ToString() == hallId)
                        {
                            Session["price_movie"] = movie.price;
                            smovie = movie;
                        }
                    }
                }
            }
            Session["movie_index"] = smovie.Movie_index;
            @Session["age"] = smovie.MoviesLibBAL.Age;
            return RedirectToAction("ChooseSeat", smovie);
        }
        public ActionResult ChooseSeat()
        {
            AccountDal dal = new AccountDal();
            int mindex = Int32.Parse(Session["movie_index"].ToString());
            List<TicketsBal> tickets = (from x in dal.Tickets where x.movie_index == mindex select x).ToList<TicketsBal>();
            List<seatBal> not_av_seats = new List<seatBal>();
            TicketsViewModel mv = new TicketsViewModel();
            mv.rseats = new List<string>();
            mv.cseats = new List<string>();
            foreach (TicketsBal ticket in tickets)
            {
                seatBal tempseat = new seatBal();
                tempseat.row = ticket.trow;
                tempseat.col = ticket.tcol;
                mv.rseats.Add(tempseat.row);
                mv.cseats.Add(tempseat.col);
                not_av_seats.Add(tempseat);
            }
            mv.soldseats = not_av_seats;
            if(Session["ListofSeats"] == null)
            {
                mv.selected_active = "0";
                var ListofSeats = new List<seatBal>();
                Session["ListofSeats"] = ListofSeats;
                mv.rseats_selected = new List<string>();
                mv.cseats_selected = new List<string>();
            }
            else
            {
                mv.selected_active = "1";
                mv.rseats_selected = new List<string>();
                mv.cseats_selected = new List<string>();
                var ListofSeats = Session["ListofSeats"] as List<seatBal>;
                foreach (seatBal s in ListofSeats)
                {
                    mv.rseats_selected.Add(s.row);
                    mv.cseats_selected.Add(s.col);
                }
            }
            return View(mv);
        }

        [HttpPost]
        public JsonResult Test(string Row, string Col)
        {
            AccountDal dal = new AccountDal();
            seatBal s = new seatBal();
            TicketsBal ticket = new TicketsBal();
            ticket.movie_index = Int32.Parse(Session["movie_index"].ToString());
            ticket.trow = Row;
            ticket.tcol = Col;
            s.row = Row;
            s.col = Col;
            if (Session["UserID"] == null)
            {
                ticket.cid = "000000000";
            }
            else
            {
                ticket.cid = Session["UserID"].ToString();
            }
            try
            {
                dal.Tickets.Add(ticket);
                dal.SaveChanges();
                List<seatBal> ListofSeats = Session["ListofSeats"] as List<seatBal>;
                ListofSeats.Add(s);
                Session["ListofSeats"] = ListofSeats;
                List<int> ticketsbagids = Session["ticketsbagids"] as List<int>;
                ticketsbagids.Add((from x in dal.Tickets
                                   where x.movie_index == ticket.movie_index
                                    && x.trow == ticket.trow && x.tcol == ticket.tcol
                                   select x.Ticket_index).First());
                Session["ticketsbagids"] = ticketsbagids;
            }
            catch (Exception ec)
            {
                System.Diagnostics.Debug.WriteLine(ec.InnerException.Message);
                return Json(new {
                    success = true,
                    code = 1 });
            }
            return Json(new
            {
                resut = "OK"
            });
        }

        [HttpPost]
        public JsonResult TestDelete(string Row, string Col)
        {
            AccountDal dal = new AccountDal();
            List<seatBal> ListofSeats = Session["ListofSeats"] as List<seatBal>;
            ListofSeats.RemoveAll(x => x.col == Col && x.row == Row);
            Session["ListofSeats"] = ListofSeats;
            TicketsBal ticket = new TicketsBal();
            ticket.movie_index = Int32.Parse(Session["movie_index"].ToString());
            ticket.trow = Row;
            ticket.tcol = Col;
            ticket = (from x in dal.Tickets
                      where x.movie_index == ticket.movie_index && x.trow == ticket.trow && x.tcol == ticket.tcol
                      select x).First();
            dal.Tickets.Remove(ticket);
            dal.SaveChanges();
            List<int> ticketsbagids = Session["ticketsbagids"] as List<int>;
            ticketsbagids.Remove(ticket.Ticket_index);
            Session["ticketsbagids"] = ticketsbagids;
            return Json(new
            {
                resut = "OK"
            });
        }


        public ActionResult Payment()
        {
            string price = Session["price_movie"].ToString();
            var ListofSeats = Session["ListofSeats"] as List<seatBal>;
            int total = Int32.Parse(price) * ListofSeats.Count;
            Session["total_price"] = total;
            return View();
        }

        public ActionResult Payment1(PaymentBal paydetails)
        {
            if (ModelState.IsValid)
            {
                AccountDal dal = new AccountDal();
                List<int> ticketsbagids = Session["ticketsbagids"] as List<int>;
                foreach (int ticket in ticketsbagids)
                {
                    PaidTicketsBal t = new PaidTicketsBal();
                    t.Ticket_index = ticket;
                    dal.PaidTickets.Add(t);
                    dal.SaveChanges();
                }
                Session["Msg"] = "the payment was succesful!";
                Session["Navigation"] = "Index";
                Session["ListofSeats"] = null;
                Session["total_price"] = null;
                return RedirectToAction("MsgPage");
            }
            return View("Payment",paydetails);
        }
    }
}