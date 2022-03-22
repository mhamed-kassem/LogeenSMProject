using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderTypeController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public TraderTypeController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("TraderTypes")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<TraderType> traderTypes = Context.TraderTypes.ToList();
            return Ok(traderTypes);
        }

        [HttpGet("{id}")]
        //[Route("TraderTypes/{id}")]
        public IActionResult GetOne(int id)
        {
            TraderType traderType = Context.TraderTypes.FirstOrDefault(s => s.Id == id);

            return Ok(traderType);
        }

        //[Route("TraderTypes/Create")]
        [HttpPost]
        public IActionResult Post(TraderType traderType)
        {
            Context.TraderTypes.Add(traderType);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("TraderTypes/update")]
        public void Put(int id, TraderType value)
        {
            var traderType = Context.TraderTypes.FirstOrDefault(s => s.Id == id);
            if (traderType != null)
            {
                Context.Entry<TraderType>(traderType).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var traderType = Context.TraderTypes.FirstOrDefault(s => s.Id == id);
            if (traderType != null)
            {
                Context.TraderTypes.Remove(traderType);
                Context.SaveChanges();
            }
        }
    }
}
