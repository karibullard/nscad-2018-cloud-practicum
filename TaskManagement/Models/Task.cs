using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace API.Models
{
    /// <summary>
    /// A configuration document for workflow tasks. 
    /// Workflows are comprised of Task objects, which are stored in a Workflow object&#39;s \&quot;tasks\&quot; map.
    /// </summary>
    [DataContract]
    public class Task
	{
        /// <summary>
        /// Gets or sets id of task
        /// </summary>
        /// <value>Uniquely identifies a task.</value>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of task
        /// </summary>
        /// <value>The display name of the task.</value>
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a task&#39;s instruction set.
        /// </summary>
        /// <value>A task&#39;s instruction set.</value>
        [DataMember(Name = "descriptions")]
        public Dictionary<UserType, string> Descriptions { get; set; }

        /// <summary>
        /// Gets or sets List of Viewers
        /// </summary>
        /// <value>Indicates the task viewer based on User type</value>
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

        /// <summary>
        /// Gets a List of Task for CloudOnsiteInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for CloudOnsiteExternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for CloudOffshoreInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for CloudOffshoreExternal
        /// </summary>
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

        #endregion CLOUD TASK LISTS

        #region DCD TASK LISTS

        /// <summary>
        /// Gets List of Tasks for DCDOnsiteInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for DCDOnsiteExternal
        /// </summary>
		public static List<Task> DCDOnsiteExternal
		{
			get { return DCDOnsiteInternal; }
		}

        /// <summary>
        /// Gets List of Tasks for DCDOffshoreInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for DCDOffshoreExternal
        /// </summary>
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

        #endregion DCD TASK LISTS

        #region SharePoint TASK LISTS

        /// <summary>
        /// Gets List of Tasks for SharePointOnsiteInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for SharePointExternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for SharePointOffshoreInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for SharePointOffshoreExternal
        /// </summary>
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

        #endregion SharePoint TASK LISTS

        #region Logic2020 TASK LISTS

        /// <summary>
        /// Gets List of Tasks for Logic2020OnsiteInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for Logic2020OnsiteExternal
        /// </summary>
		public static List<Task> Logic2020OnsiteExternal
		{
			get { return Logic2020OnsiteInternal; }
		}

        /// <summary>
        /// Gets List of Tasks for Logic2020OffshoreInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for Logic2020OffshoreExternal
        /// </summary>
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

        #endregion Logic2020 TASK LISTS

        #region Dev9 TASK LISTS

        /// <summary>
        /// Gets List of Tasks for Dev9OnsiteInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for Dev9OnsiteExternal
        /// </summary>
		public static List<Task> Dev9OnsiteExternal
		{
			get { return Dev9OnsiteInternal; }
		}

        /// <summary>
        /// Gets List of Tasks for Dev9OffshoreInternal
        /// </summary>
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

        /// <summary>
        /// Gets List of Tasks for Dev9OffshoreExternal
        /// </summary>
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

		#endregion Dev9 TASK LISTS
	}
}