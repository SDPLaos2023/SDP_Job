using Microsoft.AspNetCore.Mvc;
using SDPJob.Models;
using SDPJob.Service;
using System.Linq;
using static SDPJob.Models.DatatableModels;

namespace SDPJob.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult LookUpUser(string filter)
        {
            filter = filter == null ? "" : filter;
            UserService userService = new UserService();
            return Json(userService.GetLookup(filter));
        }
        public JsonResult GetProject(string filter)
        {
            filter = filter == null ? "" : filter;
            ProjectService projectService = new ProjectService();
            return Json(projectService.GetProject(filter));
        }
        public JsonResult GetTask(string filter)
        {
            filter = filter == null ? "" : filter;
            TaskService taskService = new TaskService();
            return Json(taskService.GetTask(filter));
        }
        public JsonResult AddTask(TaskModel taskModel)
        {
            TaskService taskService=new TaskService();
            taskService.AddTask(taskModel);
            return Json("{ 'status':'success' }");
        }
        [HttpPost]
        public JsonResult NewRequest(DtParameters dtParameters)
        {

            var searchBy = dtParameters.Search?.Value;

            var orderCriteria = "taskID";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            TaskService taskService = new TaskService();
            var result = taskService.GetNewRequest().AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Description.ToLower() != null && r.Description.ToLower().Contains(searchBy.ToLower())
                                           );
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            var filteredResultsCount = result.Count();
            var totalResultsCount = result.Count();

            return Json(new DtResult<TaskModel>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList()
            });
        }
        public JsonResult GetType(string filter)
        {
            filter = filter == null ? "" : filter;
            TaskService taskService = new TaskService();
            return Json(taskService.GetType(filter));
        }
        

    }
}
