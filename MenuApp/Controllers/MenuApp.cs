using Microsoft.AspNetCore.Mvc;
using MenuApp.Data;
using MenuApp.Models;
using Microsoft.EntityFrameworkCore;
namespace MenuApp.Controllers
{
    public class MenuApp : Controller
    {
        private readonly MenuContext _context;
        public MenuApp(MenuContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
           var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i =>i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}
