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


        #endregion



    }
}
