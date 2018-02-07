using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    public interface IWorkflowRepository : IDisposable {
        IEnumerable<Workflow> GetWorkflows();
        Workflow GetWorkflowByID(int WorkflowId);
        void InsertWorkflow(Workflow Workflow);
        void DeleteWorkflow(int WorkflowID);
        void UpdateWorkflow(Workflow Workflow);
        void Save();
    }
}