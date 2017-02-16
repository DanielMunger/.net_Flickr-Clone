using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public byte[] Image { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<User_Tag> User_Tags { get; set; }
        public virtual ICollection<Favorite_Tag> Favorite_Tags { get; set; }

        public Photo(byte[] image)
        {
            Image = image;        
        }

        public Photo()
        {

        }

    }
}
