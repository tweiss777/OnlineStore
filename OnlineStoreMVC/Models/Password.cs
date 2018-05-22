using System;
using System.ComponentModel.DataAnnotations;


namespace OnlineStoreMVC.Models
{
    public class Password
    {
        public int id{get;set;}
        
        [Required, Display(Name="Old Password")]
        public String OldPassword{get;set;}
        
        [Required,Display(Name="New Password")]
        public String NewPassword{get;set;}

        [Required,Display(Name="Confirm new password")]
        public String NewPasswordConfirmed{get;set;}


    }
    

    
}



