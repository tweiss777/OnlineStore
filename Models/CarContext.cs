using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OnlineStoreMVC.Models
{
    public class CarContext
    {
        //Connection string 
        public string ConnectionString { get; set; }


        public CarContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        #region  methods for adding, removing, updating, and obtaining data from our car table
            public Task<List<Car>> getAllCarsAsync()
            {
                return Task.Run(() =>
               {
                   List<Car> cars = new List<Car>();
                   string query = "SELECT * FROM car";//sql query represented as a string
                   MySqlConnection connection = GetConnection(); //retrieve the sqlconnection
                   MySqlCommand command = new MySqlCommand(query, connection); //initialize new sql command
                   try
                   {
                       connection.Open();
                       using (MySqlDataReader dbReader = command.ExecuteReader())
                       {
                           while (dbReader.Read())
                           {
                               Car c = new Car()
                               { //add the non nullables first
                                   Vin = Convert.ToInt32(dbReader["vin"]),
                                   Make = dbReader["make"].ToString(),
                                   Model = dbReader["model"].ToString(),
                                   Year = Convert.ToInt32(dbReader["year"]),
                                   Trimtype = dbReader["trimtype"].ToString(),
                                   Msrp = Convert.ToDouble(dbReader["msrp"])
                               };

                               //check if color is null or not
                               if (dbReader["color"] == DBNull.Value)
                               {
                                   c.Color = null;
                               }
                               else
                               {
                                   c.Color = dbReader["color"].ToString();
                               }
                               cars.Add(c);


                           }
                           dbReader.Dispose();
                       }
                   }

                   catch(MySqlException e)
                   {
                       throw new Exception("Something went wrong while running the following query", e);//throw new excpetion should something go wrong
                   }

                   finally
                   {
                       connection.Close();
                       
                   }

                   return cars; //return the list
               });
            }


        #endregion



    }
}
