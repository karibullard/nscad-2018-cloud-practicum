namespace API.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;
    using Models;

    /// <summary>
    /// Provides Workflow CRUD opertaions
    /// </summary>
    public class WorkflowRepository : IWorkflowRepository, IDisposable
    {
        private readonly string[] workflowNames = { "Cloud", "DCDO", "Dev9", "Logic2020", "SharePoint" };
        private readonly string[] workflowTypes = { "OffshoreExternal", "OnsiteExternal", "OffshoreInternal", "OnsiteInternal" };
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private readonly List<Workflow> workflows = BlobStorageUtil.GetWorkflowsFromStorage();
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowRepository"/> class.
        /// </summary>
        public WorkflowRepository()
        {
            disposed = false;
            //SeedWorkflows();
        }

        /// <summary>
        /// Gets a workflow configuration by id
        /// </summary>
        /// <param name="id">The id of the workflow to retrieve</param>
        /// <returns>A workflow object</returns>
        public Workflow Get(string id)
        {
            var workflow = workflows.Find(p => p.Id == id);
            return workflow;
        }

        /// <summary>
        /// Adds a new workflow
        /// </summary>
        /// <param name="item">The workflow item to add to storage</param>
        /// <returns>The added workflow</returns>
        public Workflow Add(Workflow item)
        {
            if (item != null)
            {
                workflows.Add(item);
                return item;
            }

            throw new ArgumentNullException(nameof(item));
        }

        /// <summary>
        /// Updates a workflow configuration
        /// </summary>
        /// <param name="item">The workflow to update</param>
        /// <returns>True if operation was successful, otherwise false</returns>
        public bool Update(Workflow item)
        {
            if (item != null)
            {
                int index = workflows.FindIndex(p => p.Id == item.Id);
                if (index == -1)
                {
                    return false;
                }

                workflows.RemoveAt(index);
                workflows.Add(item);
                return true;
            }

            throw new ArgumentNullException(nameof(item));
        }

        /// <summary>
        /// Removes a workflow from the collection by ID
        /// </summary>
        /// <param name="id">The workflow id</param>
        public void Remove(string id)
        {
            workflows.RemoveAll(p => p.Id == id);
        }

        /// <summary>
        /// Deletes a workflow from the collection via the workflow object
        /// </summary>
        /// <param name="item">The workflow to remove</param>
        public void Delete(Workflow item)
        {
            Remove(item.Id);
        }

        /// <summary>
        /// Returns all workflows
        /// </summary>
        /// <returns>All workflows from storage</returns>
        public IEnumerable<Workflow> GetAll()
        {
            return workflows;
        }

        /// <summary>
        /// Reads the workflow configurations from a resource file
        /// </summary>
        /// <remarks>The file upload will create or overwrite the existing</remarks>
        public void SeedWorkflows()
        {
            for (int i = 0; i < 5; i++)
            {
                var currentWorkflow = workflowNames[i];

                for (int j = 0; i < 4; i++)
                {
                    var workflowName = currentWorkflow + workflowTypes[j];
                    List<Task> taskList = null;
                    switch (currentWorkflow)
                    {
                        case "Cloud":
                            if (j == 0)
                            {
                                taskList = Task.CloudOffshoreExternal;
                            }
                            else if (j == 1)
                            {
                                taskList = Task.CloudOnsiteExternal;
                            }
                            else if (j == 2)
                            {
                                taskList = Task.CloudOffshoreInternal;
                            }
                            else if (j == 3)
                            {
                                taskList = Task.CloudOnsiteInternal;
                            }

                            break;
                        case "DCDO":
                            if (j == 0)
                            {
                                taskList = Task.DCDOffshoreExternal;
                            }
                            else if (j == 1)
                            {
                                taskList = Task.DCDOnsiteExternal;
                            }
                            else if (j == 2)
                            {
                                taskList = Task.DCDOffshoreInternal;
                            }
                            else if (j == 3)
                            {
                                taskList = Task.DCDOnsiteInternal;
                            }

                            break;
                        case "Dev9":
                            if (j == 0)
                            {
                                taskList = Task.Dev9OffshoreExternal;
                            }
                            else if (j == 1)
                            {
                                taskList = Task.DCDOnsiteExternal;
                            }
                            else if (j == 2)
                            {
                                taskList = Task.Dev9OffshoreInternal;
                            }
                            else if (j == 3)
                            {
                                taskList = Task.Dev9OnsiteInternal;
                            }

                            break;
                        case "Logic2020":
                            if (j == 0)
                            {
                                taskList = Task.Logic2020OffshoreExternal;
                            }
                            else if (j == 1)
                            {
                                taskList = Task.Logic2020OnsiteExternal;
                            }
                            else if (j == 2)
                            {
                                taskList = Task.Logic2020OffshoreInternal;
                            }
                            else if (j == 3)
                            {
                                taskList = Task.Logic2020OnsiteInternal;
                            }

                            break;
                        case "SharePoint":
                            if (j == 0)
                            {
                                taskList = Task.SharePointOffshoreExternal;
                            }
                            else if (j == 1)
                            {
                                taskList = Task.SharePointOnsiteExternal;
                            }
                            else if (j == 2)
                            {
                                taskList = Task.SharePointOffshoreInternal;
                            }
                            else if (j == 3)
                            {
                                taskList = Task.SharePointOnsiteInternal;
                            }

                            break;
                    }

                    var workflowConfig = new Workflow()
                    {
                        Name = workflowName,
                        Tasks = taskList
                    };
                    workflows.Add(workflowConfig);
                }
            }
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Protected IDispose implementation
        /// </summary>
        /// <param name="disposing">Indicates if item is being disposed or not</param>
        protected void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }
}
