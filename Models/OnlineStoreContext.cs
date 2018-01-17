﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace OnlineStoreMVC.Models
{

    //Context for our online store using the entity framework 
    public class OnlineStoreContext
    {
        public string ConnectionString { get; set; }

        public OnlineStoreContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        //Context incomplete...
    }
}