using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogeenStockManagement.Models;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ClientsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            //validation section
            if (id != client.Id || IsClientDataNotValid(client))
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            //validation section2
            if (IsClientDataNotValid(client))
            {
                return BadRequest();
            }
            
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            //validation section3
            if (client.ImportPayments.Count > 0 || client.SaleBills.Count > 0||client.BalanceOutstand>0) { return BadRequest(); }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        public bool IsClientDataNotValid(Client client)
        {
            //--Validation 
            /*
             * ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL,
            BalanceOutstand FLOAT,
            Phone NUMERIC(20) NOT NULL UNIQUE,
            [Address] VARCHAR(200),
            TradeName VARCHAR(100),
            TypeId INT,
            Constraint ClientPK PRIMARY KEY (ID),
            Constraint ClientTypeFK FOREIGN KEY (TypeId) REFERENCES TraderType(ID)
            */

            //foreign keys can not refer to Not Existed
            bool TypeIdExisted = _context.TraderTypes.Any(t => t.Id == client.TypeId);

            // UNIQUE Prorerty must to be Not Existed before
            bool PhoneExisted = _context.Clients.Any(c => c.Phone == client.Phone && c.Id != client.Id);

            //Not NUll properties + chech Foreign and Uniqe results
            if (client.Name == null || client.Phone == 0 || client.TypeId == null || PhoneExisted || !TypeIdExisted)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
}
