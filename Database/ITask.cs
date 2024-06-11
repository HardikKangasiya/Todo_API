using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Database
{
    public interface ITask
    {
        Response<IEnumerable<TaskModel>> GetAllTasks();
        Response<TaskModel> GetTask(int id);
        Response<TaskModel> AddTask(TaskModel task);
        Response<TaskModel> UpdateTask(int id, TaskModel task);
        Response<TaskModel> DeleteTask(int id);
    }

}
