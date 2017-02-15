using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    [Table("User_Tag")]
    public class User_Tag
    {
        [Key]
        public int User_TagId { get; set; }
        public string UserId { get; set; }
        public int PhotoId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Photo Photo { get; set; }

        public string GetId()
        {
            return this.User.Id;
        }

        public User_Tag(int photoid)
        {
            UserId = this.GetId();
            PhotoId = photoid;
        }
        public User_Tag()
        {

        }
    }
}
