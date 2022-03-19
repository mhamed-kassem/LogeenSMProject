using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
