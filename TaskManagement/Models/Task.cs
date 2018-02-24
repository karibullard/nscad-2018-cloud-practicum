namespace API.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<UserType, string> Descriptions { get; set; }

        public List<UserType> Viewers { get; set; }

        [JsonIgnore]
        private static readonly Dictionary<int, string> onsiteTasks = new Dictionary<int, string>()
{
            { 1, "Ust ID Setup" },
            { 2,  "Welcome Email" },
            { 3, "Manadatory Trainings" },
            { 4, "Background Verification" },
            { 5, "PM Sync-up" },
            { 6, "Seat & Asset Allocation" },
            { 7, "Mailbox Activation" },
            { 8, "About Me Email" },
            { 9, "Company Badge" },
            { 10, "Software Request | VPN + VIP" },
            { 11,  "Software Request | Slack" },
            { 12, "Project Tool Access | Jira" },
            { 13, "Project Tool Access | Corporate Domain" },
            { 14, "Project Tool Access | Github" },
            { 15, "Project Tool Access | SharePoint" },
            { 16, "Project Docs | Org Chart" },
            { 17, "Project Docs | About Team" },
            { 18, "Project Docs | Project Overview" }
        };

        [JsonIgnore]
        private static readonly Dictionary<int, string> offshoreTasks = new Dictionary<int, string>()
        {
            { 1, "Report to RMG" },
            { 2,  "RMG --> PM Contact Details" },
            { 3, "HR Orientation" },
            { 4, "Background Verification" },
            { 5, "Ust ID Setup" },
            { 6, "Mandatory Trainings" },
            { 7, "PM Sync-Up" },
            { 8, "About Me Email" },
            { 9, "Seat & Asset Allocation" },
            { 10, "NDA Sign-Up" },
            { 11, "Mailbox Activation" },
            { 12, "Software Request | VPN + VIP" },
            { 13, "Software Request | Slack" },
            { 14, "Project Tool Access | Jira" },
            { 15, "Project Tool Access | Corporate Domain" },
            { 16, "Project Tool Access | Github" },
            { 17, "Project Tool Access | SharePoint" },
            { 18, "Project Docs | Org Chart" },
            { 19, "Project Docs | About Team" },
            { 20, "Project Docs | Project Overview" }
        };
        
        private static Task GetOnsiteTask(int id, int onsiteTaskIndex)
        {
            if (onsiteTaskIndex > 18)
            {
                return null;
            }

            return new Task()
            {
                Id = id,
                Name = onsiteTasks[onsiteTaskIndex],
                Descriptions =
                    new Dictionary<UserType, string>
                    {
                        {
                            UserType.Employee,
                            "Employee instruction set."
                        }
                    },
                Viewers = new List<UserType>() { UserType.Employee }
            };
        }

        private static Task GetOffshoreTask(int id, int offshoreTaskIndex)
        {
            if (offshoreTaskIndex > 20)
            {
                return null;
            }

            return new Task()
            {
                Id = id,
                Name = offshoreTasks[offshoreTaskIndex],
                Descriptions =
                    new Dictionary<UserType, string>
                    {
                        {
                            UserType.Employee,
                            "Employee instruction set."
                        }
                    },
                Viewers = new List<UserType>() { UserType.Employee }
            };
        }

        #region CLOUD TASK LISTS
        public static List<Task> CloudOnsiteInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 5; i < 19; i++)
                {
                    taskList.Add(GetOnsiteTask(i, taskId));
                    taskId++;
                }

                return taskList;
            }
        }

        public static List<Task> CloudOnsiteExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    taskList.Add(GetOnsiteTask(i, taskId));
                    taskId++;
                }

                return taskList;
            }
        }

        public static List<Task> CloudOffshoreInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 6; i < 21; i++)
                {
                    taskList.Add(GetOffshoreTask(i, taskId));
                    taskId++;
                }

                return taskList;
            }
        }

        public static List<Task> CloudOffshoreExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 21; i++)
                {
                    taskList.Add(GetOffshoreTask(i, taskId));
                    taskId++;
                }

                return taskList;
            }
        }
        #endregion

        #region DCD TASK LISTS
        public static List<Task> DCDOnsiteInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    if (i < 10 || i > 15)
                    {
                        taskList.Add(GetOnsiteTask(i, taskId));
                        taskId++;

                    }
                }

                return taskList;
            }
        }

        public static List<Task> DCDOnsiteExternal
        {
            get {return DCDOnsiteInternal; }
        }

        public static List<Task> DCDOffshoreInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 6; i < 21; i++)
                {
                    if (i < 12 || i > 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }

        public static List<Task> DCDOffshoreExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 21; i++)
                {
                    if (i < 12 || i > 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }
        #endregion

        #region SharePoint TASK LISTS
        public static List<Task> SharePointOnsiteInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    if (i != 11 || i < 13 || i > 14)
                    {
                        taskList.Add(GetOnsiteTask(i, taskId));
                        taskId++;

                    }
                }

                return taskList;
            }
        }

        public static List<Task> SharePointOnsiteExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    if (i != 11)
                    {
                        taskList.Add(GetOnsiteTask(i, taskId));
                        taskId++;

                    }
                }

                return taskList;
            }
        }

        public static List<Task> SharePointOffshoreInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 6; i < 21; i++)
                {
                    if (i < 12 || i > 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }

        public static List<Task> SharePointOffshoreExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 21; i++)
                {
                    if (i < 12 || i > 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }
        #endregion

        #region Logic2020 TASK LISTS
        public static List<Task> Logic2020OnsiteInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    if (i < 10 || i < 11 || i < 13 || i > 14)
                    {
                        taskList.Add(GetOnsiteTask(i, taskId));
                        taskId++;
                    }
                }

                return taskList;
            }
        }

        public static List<Task> Logic2020OnsiteExternal
        {
            get { return Logic2020OnsiteInternal; }
        }

        public static List<Task> Logic2020OffshoreInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 6; i < 21; i++)
                {
                    if (i < 10 || i < 11 || i < 13 || i > 14)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }

        public static List<Task> Logic2020OffshoreExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 21; i++)
                {
                    if (i < 10 || i < 11 || i < 13 || i > 14)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }
        #endregion

        #region Dev9 TASK LISTS
        public static List<Task> Dev9OnsiteInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 19; i++)
                {
                    if (i != 11 || i < 13 || i > 14)
                    {
                        taskList.Add(GetOnsiteTask(i, taskId));
                        taskId++;
                    }
                }

                return taskList;
            }
        }

        public static List<Task> Dev9OnsiteExternal
        {
            get { return Dev9OnsiteInternal; }
        }

        public static List<Task> Dev9OffshoreInternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 6; i < 21; i++)
                {
                    if (i != 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }

        public static List<Task> Dev9OffshoreExternal
        {
            get
            {
                var taskList = new List<Task>();
                int taskId = 1;
                for (int i = 1; i < 21; i++)
                {
                    if (i != 12 || i < 15 || i > 16)
                    {
                        taskList.Add(GetOffshoreTask(i, taskId));
                        taskId++;
                    }

                }

                return taskList;
            }
        }
        #endregion
    }
}