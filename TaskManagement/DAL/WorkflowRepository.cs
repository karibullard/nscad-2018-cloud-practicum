namespace API.DAL
{
    using System;
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// IWorkflowRepository Implementation
    /// </summary>
    /// <remarks>Provides API endpoints access to basic CRUD for the workflow resource</remarks>
    public class WorkflowRepository : IWorkflowRepository
    {
        public Workflow Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Workflow item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Workflow item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workflow> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}