using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Views
{
    public class PersonController : Controller
    {

        public ActionResult Index()
        {
            Debug.WriteLine("Opnening a connection to data base ...");
            SQLiteConnection sqlCo = new SQLiteConnection("Data Source=C:/Users/MSI/Downloads/2022 GL3 .NET Framework TP3 - SQLite database.db;");
            try
            {
                sqlCo.Open();
                Debug.WriteLine("Connection opened !");
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM personal_info", sqlCo);
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    Debug.WriteLine("Data Reader returned " + reader.FieldCount + " comumns.");
                    if (reader.HasRows) {
                        while (reader.Read())
                        {
                            int id = (int)reader["id"];
                            string firstName = (string)reader["first_name"];
                            string lastName = (string)reader["last_name"];
                            string email = (string)reader["email"];
                            //var dateBirth = Convert.ToDateTime(reader["date_birth"]);
                            //var dateBirth = (DateTime)reader["date_birth"];
                            string image = (string)reader["image"];
                            string country = (string)reader["country"];
                            Debug.WriteLine("*id= " + id + " *first name= " + firstName + " *last name= " + lastName + " *email= " + email
                                + " *date birth= "
                                //+dateBirth
                                //+dateBirth.ToString()
                                + " *image= " + image + " *country= " + country);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Data Reader returned 0 rows");
                    }
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                sqlCo.Close();
                Debug.WriteLine("Connection closed!");
            }
            return View();
        }
        public ActionResult all()
        {
            List<Person> l = PersonalInfo.GetAllPerson();
            ViewData["peopleList"] = l;
            return View();
        }
        public ActionResult byId(int id){
            Person p= PersonalInfo.GetPersonId(id);
            ViewData["p"] = p;
           return View();
        }


        [HttpGet]
        public ActionResult search()
        {
            ViewBag.notFound = false;
            return View();
        }
        [HttpPost]
        public ActionResult search(string firstName, string country)
        {
            int id=0;
            List<Person> l = PersonalInfo.GetAllPerson();
            foreach(Person p in l)
            {
                if(p.firstName == firstName && p.country == country) id= p.id;
            }

            return RedirectToAction("byId","Person", new { id = id });
            
        }



    }
}