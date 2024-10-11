using KazanMaintenanceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KazanMaintenanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        Wsc2019Session3FinalContext context = new Wsc2019Session3FinalContext();



        public class tempTask
        {
            public int Id { get; set; }
            public string AssetName { get; set; }
            public string AssetSn { get; set; }
            public string TaskName { get; set; }
            public string ScheduleType { get; set; }
            public string ScheduleDate { get; set; }
            public int? ScheduleKilometer { get; set; }
            public bool TaskDone { get; set; }
        }
        public class tempCreateTask
        {
            public int AssetID { get; set; }
            public int TaskId { get; set; }
            public int ScheduleType { get; set; }
            public string? ScheduleDate { get; set; }
            public int? ScheduleKilometer { get; set; }
            public bool TaskDone { get; set; }
            public int? odometerReading { get; set; }
        }


        [HttpPost("createtask")]
        public IActionResult CreateTask(List<tempCreateTask> tasksList)
        {
            try
            {
                foreach (var task in tasksList)
                {
                    var newTask = new Pmtask
                    {
                        AssetId = task.AssetID,
                        TaskId = task.TaskId,
                        PmscheduleTypeId = task.ScheduleType,
                        ScheduleDate = DateOnly.Parse(task.ScheduleDate),
                        ScheduleKilometer = task.ScheduleKilometer,
                        TaskDone = task.TaskDone
                    };
                    context.Pmtasks.Add(newTask);

                    if (task.ScheduleType == 1)
                    {
                        var reading = new AssetOdometer
                        {
                            AssetId = task.AssetID,
                            ReadDate = DateOnly.FromDateTime(DateTime.Now),
                            OdometerAmount = (long)task.odometerReading
                        };
                        context.AssetOdometers.Add(reading);
                    }
                    context.SaveChanges();

                }

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost("updatetask")]
        public IActionResult UpdateTask(tempTask task)
        {
            try
            {
                var taskToUpdate = context.Pmtasks.Where(x=>x.Id == task.Id).FirstOrDefault();
                taskToUpdate.TaskDone = task.TaskDone;
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet("gettasks")]
        public IActionResult GetTasks()
        {
            
            try
            {
                var tasks = context.Pmtasks
                    .Select(x=> new
                    {
                        x.Id,
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
