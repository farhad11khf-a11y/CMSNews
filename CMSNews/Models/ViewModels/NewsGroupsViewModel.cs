using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class NewsGroupsViewModel
    {
       [Display(Name ="کد گروه خبری ")]
        public int NewsGroupId { get; set; }
        [Display(Name = "عنوان گروه خبری")]
        [Required]
        [MaxLength(200)]
        public string NewsGroupTitle { get; set; }
        [Display(Name = "تصویر خبر")]
        [MaxLength(100)]
        public string ImageName { get; set; }
        public IEnumerable<News> Newses { get; set; }
    }
}