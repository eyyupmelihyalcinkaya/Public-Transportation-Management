using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Domain.Entities
{
    public class RoleMenuPermission
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

    }
}
