using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Role
{
    public class RoleModels
    {
        [Key]
        public string RoleID { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
