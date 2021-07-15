using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace tikshoret_project.Dal
{
    public class DAL
    {
        public static string connetionString = "Data Source=OSHREYDELL5590;Initial Catalog=NetworkingProject;Integrated Security=True";
        public static System.Data.DataTable GetMovieShows(string movieName)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Movies WHERE movie_name='" + movieName + "';", cnn);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static System.Data.DataTable GetMovie(string movieName, string movieDate, string movieTime)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Movies WHERE movie_name='" + movieName + "' AND movie_time = convert(datetime,'" + movieDate + "',103)+convert(datetime,'" + movieTime + "',14);", cnn);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static System.Data.DataTable GetHall(string HallID)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Halls WHERE ID=" + HallID + ";", cnn);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}