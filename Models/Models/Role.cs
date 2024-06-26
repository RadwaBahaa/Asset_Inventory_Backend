using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }


        //RELATIONS
        public List<UserRole> UserRoles { get; set; }
    }
}
