using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
 using System.Threading.Tasks;

namespace CMSNews.Models.Models
{  
    [Table("T_ NewsGroup")]
   public class NewsGroup: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewsGroupId { get; set; }
        [Required]
        [MaxLength(200)]
        public string NewsGroupTitle { get; set; }
        [MaxLength(100)]
        public string ImageName { get; set; }
        public virtual IEnumerable<News>Newses { get; set; }
    }
}
