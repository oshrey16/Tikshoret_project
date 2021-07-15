using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tikshoret_project.Models;
using tikshoret_project.Dal;
using tikshoret_project.ModelView;

namespace tikshoret_project.Controllers
{
    public class AdminController : Controller
    {
        public bool checkadmin()
        {
            if (Session["Administrator"] != null)
            {
                return true;
            }
            return false;
        }
        public ActionResult Admin()
        {
            return RedirectToAction("AdminLogin");
        }
        public ActionResult AdminLogin()
        {
            if(Session["Administrator"] != null)
            {
                return RedirectToAction("AdminIndex");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin1()
        {
            if (ModelState.IsValid)
            {
                List<AdminBAL> account = new List<AdminBAL>();
                AccountDal dal = new AccountDal();
                string searchValueId = Request.Form["loginadmin.Id"].ToString();
                string searchValuePass = Request.Form["loginadmin.Pass"].ToString();
                AdminViewModel cvm = new AdminViewModel();
                account = (from x in dal.Admins where x.Id == (searchValueId) && x.Pass == (searchValuePass) select x).ToList<AdminBAL>();

                if (account.Count != 0)
                {

                    cvm.loginadmin = account[0];
                    Session["UserName"] = cvm.loginadmin.Cname;
                    Session["UserID"] = cvm.loginadmin.Id;
                    Session["Administrator"] = 1;
                    Session["Msg"] = "Login Success!!";
                    Session["Controller"] = "Admin";
                    Session["Navigation"] = "AdminIndex";
                    return RedirectToAction("MsgPage");
                }
                cvm.loginadmin = null;
                Session["Msg"] = "Fail Login";
                return RedirectToAction("AdminLogin");
            }
            return RedirectToAction("AdminLogin");
        }

        public ActionResult MsgPage()
        {
            return View();
        }
        // GET: Admin
        public ActionResult AdminIndex()
        {
            if (Session["Administrator"] != null)
                return View();
            else
            {
                Session["Controller"] = "Home";
                Session["Msg"] = "You Are Not Admin!!";
                Session["Navigation"] = "Index";
                return RedirectToAction("MsgPage");
            }
        }

        public ActionResult AddHall()
        {
            if (Session["Administrator"] != null)
            {
                List<HallBAL> hallslist;
                AccountDal dal = new AccountDal();
                hallslist = (from x in dal.Halls select x).ToList<HallBAL>();
                int count = hallslist.Count();
                Session["counthall"] = count + 1;
                return View();
            }
            else
            {
                Session["Controller"] = "Admin";
                Session["Msg"] = "You Are Not Admin!!";
                Session["Navigation"] = "AdminLogin";
                return RedirectToAction("MsgPage");
            }
        }
        [HttpPost]
        public ActionResult AddHall(AdminViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                HallBAL hall = new HallBAL();
                cvm.halls.ID = Int32.Parse(Session["counthall"].ToString());
                hall.ID = cvm.halls.ID;
                hall.Hall_type = cvm.halls.Hall_type;
                hall.Seats_count = cvm.halls.Seats_count;
                AccountDal dal = new AccountDal();
                dal.Halls.Add(hall);
                dal.SaveChanges();
                Session["Msg"] = "Hall Add Success!!";
                Session["Controller"] = "Admin";
                Session["Navigation"] = "AdminIndex";
                return RedirectToAction("MsgPage");
            }
            return View();
        }

        public ActionResult AddMovieLib()
        {
            if (Session["Administrator"] != null)
            {
                List<MoviesLibBAL> hallslist;
                AccountDal dal = new AccountDal();
                hallslist = (from x in dal.MoviesLib select x).ToList<MoviesLibBAL>();
                int count = hallslist.Count();
                Session["movieid"] = count + 1;
                return View();
            }
            else
            {
                Session["Controller"] = "Admin";
                Session["Msg"] = "You Are Not Admin!!";
                Session["Navigation"] = "AdminLogin";
                return RedirectToAction("MsgPage");
            }
        }

        [HttpPost]
        public ActionResult AddMovieLib(MoviesLibBAL cvm, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                cvm.Movie_Id = Session["movieid"].ToString();
                if (image1 != null)
                {
                    cvm.Picture = new byte[image1.ContentLength];
                    image1.InputStream.Read(cvm.Picture, 0, image1.ContentLength);
                }
                AccountDal dal = new AccountDal();
                dal.MoviesLib.Add(cvm);
                dal.SaveChanges();
                Session["Msg"] = "Movie Add Success!!";
                Session["Controller"] = "Admin";
                Session["Navigation"] = "AdminIndex";
                return RedirectToAction("MsgPage");
            }
            return View();
        }

        public ActionResult AddMovieShow()
        {
            List<SelectListItem> movieslist = new List<SelectListItem>();
            AccountDal dal = new AccountDal();
            
            foreach (MoviesLibBAL movie in dal.MoviesLib.ToList())
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = movie.Movie_Name,
                    Value = movie.Movie_Id,
                };
                movieslist.Add(selectList);
            }
            
            List<SelectListItem> halllist = new List<SelectListItem>();
            foreach (HallBAL hall in dal.Halls.ToList())
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = hall.ID.ToString() + ": " + hall.Hall_type,
                    Value = hall.ID.ToString()
                };
                halllist.Add(selectList);
            }

            List<SelectListItem> times = new List<SelectListItem>();
            List<int> hm = new List<int>();
            int hr = 16;
            for(int i=0;i<5;i++)
            {
                hm.Add(hr);
                hr = hr + 2;
            }
            foreach (int time in hm)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = time.ToString(),
                    Value = time.ToString()
                };
                times.Add(selectList);
            }
            MoviesViewList vc = new MoviesViewList();
            vc.Items = movieslist;
            vc.ItemsHall = halllist;
            vc.ItemsTime = times;
            vc.movie = new MovieShowBal();
            return View(vc);
        }

       [HttpPost]
        public ActionResult AddMovieShow(MoviesViewList mv)
        {
            if (ModelState.IsValid)
            {
                MovieShowBal movie = new MovieShowBal();
                AccountDal dal = new AccountDal();
                movie.Movie_Id = mv.movie.Movie_Id;
                movie.Hall_Id = mv.movie.Hall_Id;
                movie.price = mv.movie.price;
                string date = Request.Form["DateShow"].ToString();
                string hr = Request.Form["TimeList"].ToString();
                var fulldate = date + " " + hr + ":00:00";
                movie.Movie_Time = DateTime.Parse(fulldate);
                ///////////////// TODO Check If Show in DB////////////
                try
                {
                    dal.Movies.Add(movie);
                    dal.SaveChanges();
                }
                catch
                {
                    Session["Msg"] = "Error! Show Exists.Can't Add this Show. please select different values";
                    Session["Controller"] = "Admin";
                    Session["Navigation"] = "AddMovieShow";
                    return RedirectToAction("MsgPage");
                }


                Session["Msg"] = "Movie Add Success!!";
                Session["Controller"] = "Admin";
                Session["Navigation"] = "AdminIndex";
                return RedirectToAction("MsgPage");
                return View("AdminIndex");
            }
            return View("AddMovieShow");
        }

            // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
