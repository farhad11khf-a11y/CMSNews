using CMSNews.Classes.Helpers;
using CMSNews.Classes.Helpers.PersianDate;
using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSNews.Models.ViewModels
{
    public class NewsViewModel
    {
        [Display(Name ="کد خبر")]
        public int NewsId { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required]
        [MaxLength(300)]
        public string NewsTitle { get; set; }
        [Display(Name = "توضیحات خبر")]
        [Required]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "تصویر خبر")]
       
        [MaxLength(100)]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ درج")]
        [Required]
        [PersianDate(PersianDateFormat.Long)]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "وضعیت")]
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "تعداد بازدید")]
        [Required]
        public int see { get; set; }
        [Display(Name = "تعداد لایک")]
        [Required]
        public int Like { get; set; }
        [Display(Name = "گروه خبری")]
        [Required]
        public int NewsGroupId { get; set; }
        [Display(Name = "کاربر ثبت کننده ")]
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual NewsGroup NewsGroup { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }

    }
}