﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProject.Data;
using APIProject.Models;
using APIProject.Provider;
using APIProject.ViewModels;

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
        public async Task <ActionResult<List<Food>>> GetFood()
        {

            var response=await  prod.GetAll().ConfigureAwait(false);
            return response != null ? Ok(response):NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFoodById(int id)
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
            if(id<=0)
            {
                return BadRequest();
            }
            var response = await prod.GetFoodById(id).ConfigureAwait(false);
            return response!=null?Ok(response):NotFound();

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
        public async Task<IActionResult> DeleteFood(int id)
        {
            //        if (_context.Food == null)
            //        {
            //            return NotFound();
            //        }
            //        var food = await _context.Food.FindAsync(id);
            //        if (food == null)
            //        {
            //            return NotFound();
            //        

            //        _context.Food.Remove(food);
            //        await _context.SaveChangesAsync();
            if (id <= 0)
            {
                return BadRequest();
            }
            var response =await prod.DeleteFood(id).ConfigureAwait(false);
            return response!=null ? NotFound() :NoContent() ;
           

        }

        //    private bool FoodExists(int id)
        //    {
        //        return (_context.Food?.Any(e => e.FoodId == id)).GetValueOrDefault();
        //    }
        [HttpGet("NewOrder")]

        public ActionResult<List<NewOrder>> NewOrder()
        {

            return prod.ViewNewOrder();
        }
        [HttpGet("DispatchNewOrder{Id}")]
        public ActionResult<NewOrder> DispatchNewOrder(int Id)
        {

            return prod.DispatchNewOrder(Id);
        }
        [HttpDelete("DispatchOrder{Id}")]

        public async Task<IActionResult> DispatchOrder(int Id)
        {

            prod.DispatchOrder(Id);
            return NoContent();
        }
        [HttpDelete("EmptyOrder{OrderId}")]
        public IActionResult EmptyOrder(int OrderId)
        {
            prod.EmptyOrder(OrderId);
            return NoContent();
        }

    }
}
