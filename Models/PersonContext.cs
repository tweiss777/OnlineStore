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

        #endregion



    }
}
