using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    public interface ITaskRepository : IDisposable {
        IEnumerable<Task> GetTasks();
        Task GetTaskByID(int TaskId);
        void InsertTask(Task Task);
        void DeleteTask(int TaskID);
        void UpdateTask(Task Task);
        void Save();
    }
}