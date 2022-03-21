using LogeenStockManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public CategoryController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> Categorys = Context.Categories.ToList();
            return Ok(Categorys);
        }
        [HttpGet("{id}")]
        //[Route("Category/{id}")]
        public IActionResult GetOne(int id)
        {
            Category Category = Context.Categories.FirstOrDefault(ww => ww.Id == id);

            return Ok(Category);
        }
        //[Route("Category/Create")]
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            Context.Categories.Add(Category);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }
        [HttpPut("{id}")]
        //[Route("Category/Edit")]
        public void Edit(int id, Category value)
        {
            var Category = Context.Categories.FirstOrDefault(ww => ww.Id == id);
            if (Category != null)
            {
                Context.Entry<Category>(Category).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }
        [HttpDelete]
        //[Route("Category/Delete")]
        public void Delete(int Id)
        {
            var Category = Context.Categories.FirstOrDefault(ww => ww.Id == Id);
            if (Category!=null)
            {
                Context.Categories.Remove(Category);
                Context.SaveChanges();
            }
        }
    }
}
