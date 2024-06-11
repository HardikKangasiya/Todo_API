using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Results;

namespace Database
{
    public class Task : ITask
    {
        // Methods to interact with the database and perform CRUD operations
        // readonly field means it can only be assigned in the constructor of same class
        private readonly ApplicationDbContext _context;

        public Task(ApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IEnumerable<TaskModel>> GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            if (tasks == null || tasks.Count() == 0)
            {
                return new Response<IEnumerable<TaskModel>> { Success = false, StatusCode = 404, Message = "No tasks found" };
            }
            return new Response<IEnumerable<TaskModel>> { Success = true, StatusCode = 200, Message = "Tasks retrieved successfully", Data = tasks };
        }

        public Response<TaskModel> GetTask(int id)
        {
            try
            {
                var task = _context.Tasks.Find(id);
                if (task == null)
                {
                    return new Response<TaskModel> { Success = false, StatusCode = 404, Message = "Task not found" };
                }
                return new Response<TaskModel> { Success = true, StatusCode = 200, Message = "Task retrieved successfully", Data = task };
            }
            catch (Exception ex)
            {
                return new Response<TaskModel> { Success = false, StatusCode = 500, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public Response<TaskModel> AddTask(TaskModel task)
        {
            try
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return new Response<TaskModel> { Success = true, StatusCode = 201, Message = "Task added successfully", Data = task };
            }
            catch (Exception ex)
            {
                return new Response<TaskModel> { Success = false, StatusCode = 500, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public Response<TaskModel> UpdateTask(int id, TaskModel task)
        {
            try
            {
                if (id != task.Id)
                {
                    return new Response<TaskModel> { Success = false, StatusCode = 400, Message = "Task ID mismatch" };
                }

                _context.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();
                return new Response<TaskModel> { Success = true, StatusCode = 200, Message = "Task updated successfully", Data = task };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TaskModelExists(id))
                {
                    return new Response<TaskModel> { Success = false, StatusCode = 404, Message = "Task not found" };
                }
                else
                {
                    return new Response<TaskModel> { Success = false, StatusCode = 500, Message = $"An error occurred: {ex.Message}" };
                }
            }
            catch (Exception ex)
            {
                return new Response<TaskModel> { Success = false, StatusCode = 500, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public Response<TaskModel> DeleteTask(int id)
        {
            try
            {
                var task = _context.Tasks.Find(id);
                if (task == null)
                {
                    return new Response<TaskModel> { Success = false, StatusCode = 404, Message = "Task not found" };
                }

                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return new Response<TaskModel> { Success = true, StatusCode = 200, Message = "Task deleted successfully", Data = task };
            }
            catch (Exception ex)
            {
                return new Response<TaskModel> { Success = false, StatusCode = 500, Message = $"An error occurred: {ex.Message}" };
            }
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}
