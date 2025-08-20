using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Domain.Entities;
namespace internshipproject1.Application.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllAsync(CancellationToken cancellationToken); //query
        Task<Menu> GetByIdAsync(int id, CancellationToken cancellationToken); //query
        Task<Menu> AddAsync(Menu menu, CancellationToken cancellationToken); //command
        Task<Menu> UpdateAsync(Menu menu, CancellationToken cancellationToken); //command
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken); //command
        Task<bool> IsExists(int id, CancellationToken cancellationToken); //query

    }
}
