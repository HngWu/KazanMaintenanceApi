using KazanMaintenanceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KazanMaintenanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        Wsc2019Session3FinalContext context = new Wsc2019Session3FinalContext();

        [HttpGet("gettasks")]
        public IActionResult GetTasks()
        {
            
            try
            {
                var tasks = context.Pmtasks
                    .Select(x=> new
                    {
                        x.Asset.AssetName,
                        x.Asset.AssetSn,
                        x.Task.Name,
                        scheduleType=x.PmscheduleType.Name,
                        x.ScheduleDate,
                        x.ScheduleKilometer,
                        x.TaskDone

                    })
                .ToList();
                return Ok(tasks);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpGet("getassets")]
        public IActionResult GetAssets()
        {
            try
            {
                var assets = context.Assets
                    .GroupJoin(context.AssetOdometers,
                        asset => asset.Id,
                        odometer => odometer.AssetId,
                        (asset, odometers) => new
                        {
                            asset.Id,
                            asset.AssetSn,
                            asset.AssetName,
                            asset.DepartmentLocationId,
                            asset.EmployeeId,
                            asset.AssetGroupId,
                            asset.Description,
                            asset.WarrantyDate,
                            ReadDate = odometers.Select(o => o.ReadDate).FirstOrDefault(),
                            OdometerAmount = odometers.Select(o => o.OdometerAmount).FirstOrDefault()
                        })
                    .ToList();

                return Ok(assets);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
