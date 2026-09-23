using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMSNews.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفاً شماره موبایل را وارد کنید.")]
      
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}