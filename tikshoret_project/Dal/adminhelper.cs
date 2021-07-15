using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace tikshoret_project.Dal
{
    public class adminhelper
    {
        public static System.Data.DataTable GetAllTickets()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Tickets;", cnn);
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

        public static System.Data.DataTable GetTicketOfCustomer(string cid)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE cid='" + cid + "';", cnn);
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

        public static System.Data.DataTable GetAllHalls()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Halls;", cnn);
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

        public static System.Data.DataTable GetAllCustomers()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Customer;", cnn);
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

        public static System.Data.DataTable GetCustomer(string cid)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE ID=" + cid + ";", cnn);
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

        /*public static void AddHall(string type, string seats)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO NetworkingProject.dbo.Halls(""ID"",""hall_type"",""seats_count"") values (" + GetMaxHallID() + ",'" + type + "'," + seats + ");", cnn);
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static void AddMovie(string name, string date, string time, string star, int age, int hallid, string genre)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(AccountDal.connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO NetworkingProject.dbo.Movies(""movie_name"",""movie_time"",""starring"",""age"",""hall_id"",""genre"") values ('" + name + "',convert(datetime,'" + date + "',103)+convert(datetime,'" + time + "', 14),'" + star + "'," + age + "," + hallid + ",'" + genre + "');", cnn);
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}