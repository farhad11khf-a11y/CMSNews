using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Models.Models
{
    [Table("T-User")]
   public class User: BaseEntity

    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength (15)]
        public string MobileNumber  { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password  { get; set; }
        [Required]
        public DateTime RegisteDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public IEnumerable<News> Newses { get; set; }




    }
}
