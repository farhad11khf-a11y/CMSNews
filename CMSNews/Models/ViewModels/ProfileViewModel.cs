using CMSNews.Classes.Helpers.PersianDate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Display(Name = "تاریخ ثبت‌نام")]
        [PersianDate(PersianDateFormat.Long)]
        public DateTime RegisteDate { get; set; }
        [Display(Name = "تصویر پروفایل")]
        public string ImageName { get; set; }
    }
}