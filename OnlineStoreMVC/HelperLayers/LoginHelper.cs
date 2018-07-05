using System;
using System.Collections.Generic;
using System.Security.Claims;
using OnlineStoreMVC.Models;

namespace OnlineStoreMVC.HelperLayers
{

    /// <summary>
    /// Login helper. Consists of helper methods to authenticate user
    /// </summary>
    public class LoginHelper
    {
        //delcare Claims List
        private List<Claim> claims;

        //delcare ClaimsIdentity 
        private ClaimsIdentity claimsIdentity;

        //delcare principles
        private ClaimsPrincipal claimsPrincipal;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OnlineStoreMVC.HelperLayers.LoginHelper"/> class.
        /// </summary>
        /// <param name="person">Takes in Person object</param>
        public LoginHelper(Person person)
        {
            claims = new List<Claim>{
                new Claim(ClaimTypes.Email, person.Email),
                new Claim(ClaimTypes.Name, person.Firstname + " " + person.Lastname)
            };
            claimsIdentity = new ClaimsIdentity(claims, "login");
            claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        }


        /// <summary>
        /// Gets the claims principal used for authenticating a user.
        /// </summary>
        /// <returns>The claims principal if found.
        /// Null if nothing is present.</returns>
        public ClaimsPrincipal GetClaimsPrincipal(){
            if(claimsPrincipal!= null)return claimsPrincipal;
            return null;
        }

    }
}
