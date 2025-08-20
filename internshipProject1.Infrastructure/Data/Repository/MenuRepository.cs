using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Menu> AddAsync(Menu menu, CancellationToken cancellationToken)
        {
            var newMenu = new Menu
            {
                Name = menu.Name,
                Url = menu.Url,
                DisplayOrder = menu.DisplayOrder,
                ParentMenuId = menu.ParentMenuId
            };
            await _context.Menu.AddAsync(newMenu, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newMenu;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var menu = await _context.Menu.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
            if (menu == null)
            {
                return false;
            }
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<Menu>> GetAllAsync(CancellationToken cancellationToken)
        {
            var menus = await _context.Menu.ToListAsync(cancellationToken);
            if (menus == null || !menus.Any())
            {
                throw new Exception("No menus found.");
            }
            return menus;
        }

        public async Task<Menu> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var menu = await _context.Menu.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
            if (menu == null)
            {
                throw new Exception($"Menu with ID {id} not found.");
            }
            return menu;
        }

        public async Task<bool> IsExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Menu.AnyAsync(m=>m.Id == id, cancellationToken);
        }

        public async Task<Menu> UpdateAsync(Menu menu, CancellationToken cancellationToken)
        {
            var existsMenu = await _context.Menu.FirstOrDefaultAsync(m => m.Id == menu.Id, cancellationToken);
            if (existsMenu == null)
            {
                throw new Exception($"Menu with ID {menu.Id} not found.");
            }
            existsMenu.Name = menu.Name;
            existsMenu.Url = menu.Url;
            existsMenu.DisplayOrder = menu.DisplayOrder;
            existsMenu.ParentMenuId = menu.ParentMenuId;
            await _context.SaveChangesAsync(cancellationToken);
            return existsMenu;
        }
    }
}
