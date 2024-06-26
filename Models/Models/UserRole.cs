using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserRole
    {
        [Key, Column(Order = 0)]  // Specify UserId as part of the composite key
        public int UserID { get; set; }

        [Key, Column(Order = 1)]  // Specify RoleId as part of the composite key
        public int RoleID { get; set; }

        //RELATIONS
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
