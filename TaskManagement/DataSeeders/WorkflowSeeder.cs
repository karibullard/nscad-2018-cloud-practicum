using System.Collections.Generic;
using API.Models;
using Newtonsoft.Json;

namespace API.DataSeeders
{
	public class WorkflowSeeder
	{
		private readonly string[] workflowNames = { "Cloud", "DCDO", "Dev9", "Logic2020", "SharePoint" };
		private readonly string[] workflowTypes = { "OffshoreExternal", "OnsiteExternal", "OffshoreInternal", "OnsiteInternal" };

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

					//var workflowConfig = new Workflow()
					//{
					//	Name = workflowName,
					//	Tasks = taskList
					//};
				}
			}
		}
	}

	/// <summary>
	/// Represents a discrete action taken by or assigned to a user.
	/// </summary>
	public class Task
	{
		/// <summary>
		/// Gets or sets id of task
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name of task
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets
		/// </summary>
		public Dictionary<UserType, string> Descriptions { get; set; }

		/// <summary>
		/// Gets or sets List of Viewers
		/// </summary>
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