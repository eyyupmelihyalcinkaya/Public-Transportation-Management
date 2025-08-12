using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
namespace internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByRoleId
{
    public class GetPermissionByRoleIdQueryResponse
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        // Serileştirme döngülerini önlemek için sadece gerekli, düz alanlar
        public string MenuName { get; set; } = string.Empty;
        public string MenuUrl { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
