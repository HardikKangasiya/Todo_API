using Database;
using Models;
using System.Net;
using System.Web.Http;

namespace Todo.Controllers
{
    public class TodoController : ApiController
    {
        private readonly ITask _task;

        public TodoController(ITask task)
        {
            _task = task;
        }

        // GET: api/Todo
        [HttpGet]
        public IHttpActionResult GetTasks()
        {
            var response = _task.GetAllTasks();
            if (response.StatusCode == 200)
            {
                return Content((HttpStatusCode)response.StatusCode, response);
            }
            return Content((HttpStatusCode)response.StatusCode, response.Message);
        }

        // GET: api/Todo/5
        [HttpGet]
        public IHttpActionResult GetTask(int id)
        {
            var response = _task.GetTask(id);
            if (response.StatusCode == 200)
            {
                return Content((HttpStatusCode)response.StatusCode, response);
            }
            return Content((HttpStatusCode)response.StatusCode, response);
        }

        // POST: api/Todo
        [HttpPost]
        public IHttpActionResult AddTask(TaskModel task)
        {
            var response = _task.AddTask(task);
            if (response.StatusCode == 201)
            {
                return Content((HttpStatusCode)response.StatusCode, response);
            }
            return Content((HttpStatusCode)response.StatusCode, response.Message);
        }

        // PUT: api/Todo/5
        [HttpPut]
        public IHttpActionResult UpdateTask(int id, TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.Id)
            {
                return BadRequest("Id is required and can't change manually");
            }

            var response = _task.UpdateTask(id, task);
            if (response.StatusCode == 200)
            {
                return Content((HttpStatusCode)response.StatusCode, response);
            }
            return Content((HttpStatusCode)response.StatusCode, response.Message);
        }

        // DELETE: api/Todo/5
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            var response = _task.DeleteTask(id);
            if (response.StatusCode == 200)
            {
                return Content((HttpStatusCode)response.StatusCode, response);
            }
            return Content((HttpStatusCode)response.StatusCode, response.Message);
        }
    }

}
