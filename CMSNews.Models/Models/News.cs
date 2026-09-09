using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSNews.Models.Models
{
    [Table("T-News")]
   public class News: BaseEntity
    {
        [Key]
        public int NewsId  { get; set; }
        [Required]
        [MaxLength(300)]
        public string NewsTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImageName { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int see { get; set; }
        [Required]
        public int Like { get; set; }
        [Required]
        public int NewsGroupId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public NewsGroup NewsGroup { get; set; }
        public IEnumerable<Comment> Comments { get; set; }




    }
}
