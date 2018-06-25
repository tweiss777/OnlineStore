﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreMVC.HelperLayers
{
    public class Cookie
    {
        //Cookies are key value pairs in dotnet
        private String cookieKey;
        private String cookieVal;
        private CookieOptions options;


        ///<summary>Cookie constructor</summary>
        ///<param name="key">the cookie key</param>
        ///<param name="value">the cookie value</param>
        #region cookie constructors
        public Cookie(string key, string value)
        {
            cookieKey = key;
            cookieVal = value;
            options = new CookieOptions();
        }

        //CONSTRUCTOR BELOW MIGHT NOT BE NEEDED
        //<summary>Cookie constructor</summary>
        //<param name="key">the cookie key</param>
        //<param name="value">the cookie value</param>
        //<param name="cookie options"> the cookie options>
        //public Cookies(string key, string value, CookieOptions options)
        //{
            //cookieKey = key;
            //cookieVal = value;
            //this.options = options;
        //}
        #endregion


         ///<summary>Sets the cookie</summary>
         ///<returns> true if success, false if otherwise</returns>

        public bool SetCookie(int? expireTime)
        {
            //If you have an expiretime set it.
            bool success = false; //success indicator
            return success;
        }
        

    }
}
