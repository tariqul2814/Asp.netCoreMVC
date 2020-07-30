using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyTutorial.Model.ViewModel
{
    public class UserVModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<RoleVModel> UserInRoles { get; set; }
    }
}
