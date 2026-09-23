using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class UserViewModel
    {
   [Display(Name ="کد کاربر")]
        public int UserId { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        [Display(Name = "پسورد")]
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تاریخ ثبت نام")]

        public DateTime RegisteDate { get; set; }
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
      
        public virtual IEnumerable<News> Newses { get; set; }
    }
}