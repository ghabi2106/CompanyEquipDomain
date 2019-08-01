using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public User() : base()
        {
            //Groups = new List<Group>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public bool Disabled { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
