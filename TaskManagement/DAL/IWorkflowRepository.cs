namespace API.DAL
{
    using System.Collections.Generic;
    using Models;

    public interface IWorkflowRepository
    {
        IEnumerable<Workflow> GetAll();

        Workflow Get(int id);

        Workflow Add(Workflow item);

        bool Update(Workflow item);

        void Remove(int id);

        void Delete(Workflow item);
    }
}