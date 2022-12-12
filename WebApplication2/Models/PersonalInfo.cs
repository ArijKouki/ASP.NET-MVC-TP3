using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Diagnostics;

namespace WebApplication2.Models
{
    public class PersonalInfo
    {
        public static List<Person> GetAllPerson()
        {
            List<Person> l = new List<Person>();
            SQLiteConnection sqlCo = new SQLiteConnection("Data Source=C:/Users/MSI/Downloads/2022 GL3 .NET Framework TP3 - SQLite database.db;");
            try
            {
                sqlCo.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM personal_info", sqlCo);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string firstName = (string)reader["first_name"];
                        string lastName = (string)reader["last_name"];
                        string email = (string)reader["email"];
                        //var dateBirth = Convert.ToDateTime(reader["date_birth"]).ToString("dd/MM/yyyy");
                        //DateTime dateBirth = Convert.ToDateTime(reader["date_birth"]);
                        DateTime dateBirth = new DateTime();
                        string image = (string)reader["image"];
                        string country = (string)reader["country"];

                        Person p = new Person(id, firstName, lastName, email, dateBirth, image, country);
                        l.Add(p);
                    }


                }
            }

            catch (Exception e)
            {
            }
            finally
            {
                sqlCo.Close();
            }

            return l;
        }



        public static Person GetPersonId(int id)
        {
            Person p=null ;
            SQLiteConnection sqlCo = new SQLiteConnection("Data Source=C:/Users/MSI/Downloads/2022 GL3 .NET Framework TP3 - SQLite database.db;");
            try
            {
                sqlCo.Open();
                Debug.WriteLine("Connection opened !");
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM personal_info WHERE id=@id", sqlCo);
                command.Parameters.AddWithValue("@id", id.ToString());

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    Debug.WriteLine("Query succeeded !");

                    while (reader.Read())
                    {
                    string firstName = (string)reader["first_name"];
                    Debug.WriteLine(firstName);

                    string lastName = (string)reader["last_name"];
                        string email = (string)reader["email"];
                        //var dateBirth = Convert.ToDateTime(reader["date_birth"]).ToString("dd/MM/yyyy");
                        //DateTime dateBirth = Convert.ToDateTime(reader["date_birth"]);
                        DateTime dateBirth = new DateTime();
                        string image = (string)reader["image"];
                        string country = (string)reader["country"];

                        p = new Person(id, firstName, lastName, email, dateBirth, image, country);
                    Debug.WriteLine("person assigned");
                    }


                }
            }

            catch (Exception e)
            {
            }
            finally
            {
                sqlCo.Close();
                Debug.WriteLine("Connection closed !");

            }

            return p;


        }





    }
}