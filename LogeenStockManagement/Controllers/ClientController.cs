using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ClientController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Client")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Client> clients = Context.Clients.ToList();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        //[Route("Client/{id}")]
        public IActionResult GetOne(int id)
        {
            Client client = Context.Clients.FirstOrDefault(s => s.Id == id);

            return Ok(client);
        }

        //[Route("Clients/Create")]
        [HttpPost]
        public IActionResult Post(Client client)
        {
            Context.Clients.Add(client);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Clients/update")]
        public void Put(int id, Client value)
        {
            var client = Context.Clients.FirstOrDefault(s => s.Id == id);
            if (client != null)
            {
                Context.Entry<Client>(client).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var client = Context.Clients.FirstOrDefault(s => s.Id == id);
            if (client != null)
            {
                Context.Clients.Remove(client);
                Context.SaveChanges();
            }
        }
    }
}
