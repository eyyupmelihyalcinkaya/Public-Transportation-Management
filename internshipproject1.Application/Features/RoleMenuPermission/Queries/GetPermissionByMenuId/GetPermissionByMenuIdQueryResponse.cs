using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByMenuId
{
    public class GetPermissionByMenuIdQueryResponse
    {
        public int MenuId { get; set; }
        public List<RolePermissionDTO> RolePermissions { get; set; } = new List<RolePermissionDTO>();
        public string Message { get; set; }
        public bool HasAnyPermission { get; set; }

    }

    public class RolePermissionDTO 
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool HasAnyPermission => CanRead || CanCreate || CanUpdate || CanDelete;
        public bool HasFullAccess => CanRead && CanCreate && CanUpdate && CanDelete;

    }
}
