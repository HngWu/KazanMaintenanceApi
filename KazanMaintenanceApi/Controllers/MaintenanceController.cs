using KazanMaintenanceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KazanMaintenanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        Wsc2019Session3FinalContext context = new Wsc2019Session3FinalContext();

        [HttpGet]
        public IActionResult GetTasks()
        {
            
            try
            {
                var tasks = context.Tasks
                .ToList();
                return Ok(tasks);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
