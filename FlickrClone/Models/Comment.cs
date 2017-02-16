using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
    }
}
