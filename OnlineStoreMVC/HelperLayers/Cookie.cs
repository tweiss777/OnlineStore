using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
        public Cookie(string key, string value)
        {
            cookieKey = key;
            cookieVal = value;
            options = new CookieOptions();
        }

         ///<summary>Sets the cookie</summary>
         ///<returns> true if success, false if otherwise</returns>

        public bool SetCookie(int? expireTime)
        {
            //If you have an expiretime set it.
            bool success = false; //success indicator

            //Add an expire time to the cookie if value has been passed in expiretime
            if(expireTime.HasValue)
            {
                options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                options.Expires = DateTime.Now.AddSeconds(60);
            }

            //set the cookie
            Response.Cookies.Append(cookieKey, cookieVal, options);

            return success;
        }
        

    }
}
