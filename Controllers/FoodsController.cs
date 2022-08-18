using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProject.Data;
using APIProject.Models;
using APIProject.Provider;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IProvider prod;
        public FoodsController(IProvider prod)
        {
            this.prod = prod;
        }

        // GET: api/Foods
        [HttpGet]
        public ActionResult<List<Food>> GetFood()
        {
         
            return prod.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            //if (_context.Food == null)
            //{
            //    return NotFound();
            //}
            //var food = await _context.Food.FindAsync(id);

            //if (food == null)
            //{
            //    return NotFound();
            //}

            //return food;
            return prod.GetFoodById(id);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditFood(int id, Food food)
        {
            //if (id != food.FoodId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(food).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //if (!FoodExists(id))
            //{
            //    return NotFound();
            //}
            //    else
            //    {
            //        throw;
            //    }
            //}
            prod.EditFood(id, food);
            return NoContent();
        }

        // POST: api/Foods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {

            prod.AddNewFood(food);
            return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id,Food food)
        {
            //        if (_context.Food == null)
            //        {
            //            return NotFound();
            //        }
            //        var food = await _context.Food.FindAsync(id);
            //        if (food == null)
            //        {
            //            return NotFound();
            //        }

            //        _context.Food.Remove(food);
            //        await _context.SaveChangesAsync();
            prod.DeleteFood(id);

            return NoContent();
        }

        //    private bool FoodExists(int id)
        //    {
        //        return (_context.Food?.Any(e => e.FoodId == id)).GetValueOrDefault();
        //    }
    }
}
