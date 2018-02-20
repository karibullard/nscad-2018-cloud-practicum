using System.Threading.Tasks;

namespace API.DAL
{
    using System.Collections.Generic;
    using Models;

    public interface IWorkflowRepository
    {
        IEnumerable<WorkflowGetAllDTO> GetAll();

        Task<Workflow> GetAsync(string id);

        Workflow Add(Workflow item);

        bool Update(Workflow item);

        void Remove(string id);

        void Delete(Workflow item);

        void Remove(Workflow workflow);
    }
}