using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class CommentsViewMdel
    {
        [Display(Name="کد کامنت")]
        public int CommentId { get; set; }
        [Display(Name = "متن کامنت" )]
        [DataType(DataType.MultilineText)]
        [Required]
        [MaxLength(2000)]
        public string CommentText { get; set; }
        [Display(Name = "نام ")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Display(Name = "ایمیل کاربر")]
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }
     
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = " کد کاربر")]
        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}