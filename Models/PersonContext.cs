using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
namespace OnlineStoreMVC.Models
{

    //Context for our online store using the entity framework 
    public class PersonContext
    {
        //the context is considered the data tier of our application
        public string ConnectionString { get; set; }

        public PersonContext(string ConnectionString)
        { //referenced in Startup.cs
            this.ConnectionString = ConnectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        #region methods for adding, removing, updating, and obtaining data from our users table
        public Task<List<Person>> getAllUsersAsync()
        {

            //throw new NotImplementedException();
            return Task.Run( ()=>
            {
                List<Person> personList = new List<Person>();
                
                MySqlConnection connection = GetConnection(); //initialize a new sql connection
                string query = "SELECT * FROM person;"; //SQL QUERY
                MySqlCommand command = new MySqlCommand(query,connection); //where the connectionstring and the query is stored
                try
                {
                    connection.Open();
                    using(MySqlDataReader dbReader = command.ExecuteReader())
                    {   //read through all the entries in the database
                        while(dbReader.Read())
                        {
                         //create a new person object
                         Person p = new Person(){
                             //store information in here..
                             UserID = Convert.ToInt32(dbReader["userID"]),
                             Password = dbReader["psswrd"].ToString(),
                             Firstname = dbReader["fname"].ToString(),
                             Lastname = dbReader["lname"].ToString(),
                             Addr1 = dbReader["addr1"].ToString(),
                             Email = dbReader["email"].ToString()
                         };

                        if(dbReader["addr2"] == DBNull.Value)
                        {
                            p.Addr2 = null;
                        }
                        else
                        {
                            p.Addr2 = dbReader["addr2"].ToString();
                        }
                        

                         personList.Add(p);//add object to personlist
                        }
                        dbReader.Dispose();//close the reader
                    }
                }
                catch(MySqlException sqlException)
                {
                    throw new Exception("Something went wrong while reading in the database",sqlException);
                }
                finally
                {
                    connection.Close();                
                }


                return personList;

            });

        }

        public Task<bool> InsertNewUserAsync(Person p)
        { //Untested
            return Task.Run(() =>
           {
               bool was_success = false; //used to indicate if the query successfully executed
               MySqlConnection connection = GetConnection();
               string query = "INSERT INTO person (psswd,fname,lname,addr1,addr2,email) VALUES @password, @first, @last, @a1, @a2, @emailAddr;";

               MySqlCommand command = new MySqlCommand(query, connection);

               //sql command parameters
               command.Parameters.AddWithValue("@password", p.Password);
               command.Parameters.AddWithValue("@first", p.Firstname);
               command.Parameters.AddWithValue("@last", p.Lastname);
               command.Parameters.AddWithValue("@a1", p.Addr1);

               //check if addresss 2 is null.
               if(p.Addr2 == null)
               {
                   command.Parameters.AddWithValue("@a2", DBNull.Value);
               }
               else
               {
                   command.Parameters.AddWithValue("@a2", p.Addr2);
               }
               command.Parameters.AddWithValue("@emailAddr", p.Email);


               try
               {
                   connection.Open();
                   int rowsAffected = command.ExecuteNonQuery();
                   if(rowsAffected == 1)
                   {
                       was_success = true; //if the # of rows affected was equal to one
                   }
               }
               catch(MySqlException sqlException)
               {
                   throw new Exception("Something went wrong while inserting", sqlException);
               }

               finally
               {
                   connection.Close();
               }

               //return the was_success boolean
               return was_success;
           });
        }

        #endregion



    }
}
