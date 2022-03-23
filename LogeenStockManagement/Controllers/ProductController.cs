using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

/* watch the Desktop video and understand the logic
 * 
 * its not just a CRUD there is alot of business logic to do 
 * 
 * it is not just a task.
 * 
 * Rules to follow must follow 
 * 
 * Never change any thing any where in the project its not your own it our own. if do Conflicts occur
 * -just and only just change and write in your file in your Controller only and just only
 * 
 * No packages adding 
 * No settings changing 
 * do not touch any class just and only just your owns 
 * do not add service in the program.cs dot touch it any
 * 
 * ******* just focus on your controller on your Task *********
 * 
 */
namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ProductController(LogeenStockManagementContext context)
        {
            Context = context;
        }

        //[Route("products")]
        public IActionResult GetAll()
        {
            List<Product> products = Context.Products.ToList();
            return Ok(products);
        }

        [Route("products/Create")]
        public IActionResult Create(Product product)
        {
            Context.Products.Add(product);
            return Ok("Created Successfully");
        }


    }
}
