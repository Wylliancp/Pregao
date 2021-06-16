using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiFinance.Data;
using ApiFinance.Models;
using System.Collections.Generic;

namespace ApiFinance.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var employee = new User { Id = 1, Username = "robin", Password = "robin", Role = "employee" };
            var manager = new User { Id = 2, Username = "batman", Password = "batman", Role = "manager" };
            // var category = new Category { Id = 1, Title = "Informática" };
            // var product = new Product { Id = 1, Category = category, Title = "Mouse", Price = 299, Description = "Mouse Gamer" };
            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Ativos.Add(new Ativo(){Id = 1, Pregao = new List<Pregao>()});
            context.Pregaos.Add(new Pregao());
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}