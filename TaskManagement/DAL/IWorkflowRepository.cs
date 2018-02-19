namespace API.DAL
{
    using System.Collections.Generic;
    using Models;

    public interface IWorkflowRepository
    {
        IEnumerable<Workflow> GetAll();

        Workflow Get(string id);

        Workflow Add(Workflow item);

        bool Update(Workflow item);

        void Remove(string id);

        void Delete(Workflow item);
    }
}