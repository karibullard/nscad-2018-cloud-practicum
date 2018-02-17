namespace API.DAL
{
    using System;
    using System.Collections.Generic;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Models;
    using Services;

    public class WorkflowRepository : IWorkflowRepository, IDisposable
    {
        private readonly string[] workflowNames = { "Cloud", "DCDO", "Dev9", "Logic2020", "SharePoint" };
        private readonly string[] workflowTypes = { "OffshoreExternal", "OnsiteExternal", "OffshoreInternal", "OnsiteInternal" };

        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;
        private List<Workflow> workflows;
        private int nextId;
        private readonly IBlobService service = new BlobService();

        public WorkflowRepository()
        {
            nextId = 1;
            workflows = new List<Workflow>();
            SeedWorkflows();

        }

        public Workflow Get(int id)
        {
            return workflows.Find(p => p.Id == id);
        }

        public Workflow Add(Workflow item)
        {
            if(item != null)
            {
                item.Id = nextId++;
                workflows.Add(item);
                return item;
            }

            throw new ArgumentNullException(nameof(item));
        }

        public bool Update(Workflow item)
        {
            if(item != null)
            {
                int index = workflows.FindIndex(p => p.Id == item.Id);
                if(index == -1)
                {
                    return false;
                }

                workflows.RemoveAt(index);
                workflows.Add(item);
                return true;
            }

            throw new ArgumentNullException(nameof(item));
        }

        public void Remove(int id)
        {
            workflows.RemoveAll(p => p.Id == id);
        }

        public void Delete(Workflow item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workflow> GetAll()
        {
            return workflows;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
        public void Dispose()
        {
        }

        /// <summary>
        /// Reads the workflow configurations from a resource file
        /// </summary>
        /// <remarks>The file upload will create or overwrite the existing</remarks>
        public void SeedWorkflows()
        {
            for(int i = 0; i < 5; i++) {
                var currentWorkflow = workflowNames[i];

                for(int j = 0; i < 4; i++) {
                    var workflowName = currentWorkflow + workflowTypes[j];
                    List<Task> taskList = null;
                    switch(currentWorkflow) {
                        case "Cloud":
                            if(j == 0) {
                                taskList = Task.CloudOffshoreExternal;
                            } else if(j == 1) {
                                taskList = Task.CloudOnsiteExternal;
                            } else if(j == 2) {
                                taskList = Task.CloudOffshoreInternal;
                            } else if(j == 3) {
                                taskList = Task.CloudOnsiteInternal;
                            }

                            break;
                        case "DCDO":
                            if(j == 0) {
                                taskList = Task.DCDOffshoreExternal;
                            } else if(j == 1) {
                                taskList = Task.DCDOnsiteExternal;
                            } else if(j == 2) {
                                taskList = Task.DCDOffshoreInternal;
                            } else if(j == 3) {
                                taskList = Task.DCDOnsiteInternal;
                            }

                            break;
                        case "Dev9":
                            if(j == 0) {
                                taskList = Task.Dev9OffshoreExternal;
                            } else if(j == 1) {
                                taskList = Task.DCDOnsiteExternal;
                            } else if(j == 2) {
                                taskList = Task.Dev9OffshoreInternal;
                            } else if(j == 3) {
                                taskList = Task.Dev9OnsiteInternal;
                            }

                            break;
                        case "Logic2020":
                            if(j == 0) {
                                taskList = Task.Logic2020OffshoreExternal;
                            } else if(j == 1) {
                                taskList = Task.Logic2020OnsiteExternal;
                            } else if(j == 2) {
                                taskList = Task.Logic2020OffshoreInternal;
                            } else if(j == 3) {
                                taskList = Task.Logic2020OnsiteInternal;
                            }

                            break;
                        case "SharePoint":
                            if(j == 0) {
                                taskList = Task.SharePointOffshoreExternal;
                            } else if(j == 1) {
                                taskList = Task.SharePointOnsiteExternal;
                            } else if(j == 2) {
                                taskList = Task.SharePointOffshoreInternal;
                            } else if(j == 3) {
                                taskList = Task.SharePointOnsiteInternal;
                            }

                            break;
                    }

                    var workflowConfig = new Workflow() {
                        Id = nextId,
                        Name = workflowName,
                        Tasks = taskList
                    };
                    workflows.Add(workflowConfig);
                }
            }
        }
    }
}
