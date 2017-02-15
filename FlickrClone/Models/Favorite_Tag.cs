using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    [Table("Favorite_Tag")]
    public class Favorite_Tag
    {
        [Key]
        public int Favorite_TagId { get; set; }
        public string UserId { get; set; }
        public int PhotoId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Photo Photo { get; set; }

        public string GetId()
        {
            return this.User.Id;
        }
        
        public Favorite_Tag(int photoid)
        {
            UserId = this.GetId();
            PhotoId = photoid;
        }
         public Favorite_Tag()
        {

        }
    }
}
