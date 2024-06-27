using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{

    public class UserRole
    {
       
        public int UserID { get; set; }

        public int RoleID { get; set; }

        //RELATIONS
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
