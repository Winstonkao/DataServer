using Newtonsoft.Json;
using Npgsql;
using DataServer.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataServerApi
{
    [Produces("application/json")]
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        // GET: api/Companies
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            using (StockContext context = new StockContext())
            {
                return context.Companies.ToList<Company>();
            }             
        }

        // GET: api/Companies/5
        [HttpGet("{symbol}", Name = "Get")]
        public IActionResult Get(string symbol)
        {
            using (StockContext context = new StockContext())
            {
                var foundCompany = context.Companies.Where(comp => comp.symbol == symbol);
                if (foundCompany.Count() == 0 )
                {
                    return NotFound();
                }

                return Ok(foundCompany.Single());
            }
        }
        
        // POST: api/Companies
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            Company newCompany = JsonConvert.DeserializeObject<Company>(value);
            if (string.IsNullOrEmpty(newCompany.symbol))
            {
                return BadRequest();
            }
            
            string message  = JsonConvert.SerializeObject(new {
                type = "Create",
                company = newCompany
            });

            Rabbit.Send("DataServerCompany", message);

            return Ok(newCompany);
        }
        
        // PUT: api/Companies/5
        [HttpPut("{symbol}")]
        public IActionResult Put(string symbol, [FromBody]string value)
        {
              
            return Ok();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
