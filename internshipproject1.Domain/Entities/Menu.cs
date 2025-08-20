using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }

        public int? ParentMenuId { get; set; }
        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
        public virtual ICollection<RoleMenuPermission> RoleMenuPermissions { get; set; } = new List<RoleMenuPermission>();
    }
}
