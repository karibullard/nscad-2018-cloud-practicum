using System.Collections.Generic;
using Swashbuckle.Examples;

namespace API.Models
{
	public class WorkflowPost201RequestExample : IExamplesProvider
	{
		public object GetExamples()
		{
			return new Workflow
			{
				Id = "CloudOffshoreExternal",
				Name = "CloudOffshoreExternal",
				Description = "Onboarding tasks for offshore external Cloud employees.",
				Tasks = new List<WorkflowTask>
				{
					new WorkflowTask
					{
						Id = 1,
						Name = "Report to RMG",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Fill out request form xxxx and send to HR." },
							{ UserType.employee, "Ask manager to send ID creation request to HR." },
							{ UserType.hr, "Fill out request form xxxx and send to tech dept for id creation.  Upon response, notify manager of new employee id." },
							{ UserType.it, "generate new id and return id information to HR." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee,
							UserType.hr,
							UserType.it
						}
					},
					new WorkflowTask
					{
						Id = 2,
						Name = "Welcome Email",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "hr will send welcome email to new hire." },
							{ UserType.employee, "hr will send welcome email to new hire." },
							{ UserType.hr, "Send welcome email to new hire." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee,
							UserType.hr
						}
					},
					new WorkflowTask
					{
						Id = 3,
						Name = "Mandatory Trainings",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Sign new hire up for applicable trainings." },
							{ UserType.employee, "Obtain list of mandatory trainings from your manager.  Complete each training." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee
						}
					},
					new WorkflowTask
					{
						Id = 4,
						Name = "Background Verification",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Verify with hr that employee's background verification is complete." },
							{ UserType.employee, "Verify with your manager that your background verification is completed." },
							{ UserType.hr, "Send employee information to xxx for background verification.  Notify Manager when complete." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee,
							UserType.hr
						}
					},
					new WorkflowTask
					{
						Id = 5,
						Name = "PM Sync-Up",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Meet with new-hire." },
							{ UserType.employee, "Meet with manager." },
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee
						}
					},
					new WorkflowTask
					{
						Id = 6,
						Name = "Seat & Asset Allocation",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Submit form xxx to tech for seat and asset allocation." },
							{ UserType.employee, "Work with manager to get a seat and computer." },
							{ UserType.it, "Install computer at new employee's assigned seating area." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee,
							UserType.it
						}
					},
					new WorkflowTask
					{
						Id = 7,
						Name = "Mailbox Activation",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Submit form xxx to tech for Mailbox Activation." },
							{ UserType.employee, "Work with manager to get your mailbox setup." },
							{ UserType.it, "Setup new employee's mailbox." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee,
							UserType.it
						}
					},
					new WorkflowTask
					{
						Id = 8,
						Name = "About Me Email",
						Descriptions = new Dictionary<UserType, string>
						{
							{ UserType.manager, "Work with new hire on what content should be in about me email they send out." },
							{ UserType.employee, "Send email to team with information about yourself to introduce yourself to the team." }
						},
						Viewers = new List<UserType>
						{
							UserType.manager,
							UserType.employee
						}
					},
				}
			};
			//return new Workflow
			//{
			//	Id = "CloudOffshoreExternal",
			//	Name = "CloudOffshoreExternal",
			//	Description = "Onboarding tasks for offshore external Cloud employees.",
			//	Tasks = new List<WorkflowTask>
			//	{
			//		new WorkflowTask
			//		{
			//			Id = 1,
			//			Name = "Report to RMG",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Fill out request form xxxx and send to HR." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Ask manager to send ID creation request to HR." },
			//				new TaskDescription { Viewer = UserType.HR, Instructions = "Fill out request form xxxx and send to tech dept for id creation.  Upon response, notify manager of new employee id." },
			//				new TaskDescription { Viewer = UserType.IT, Instructions = "generate new id and return id information to HR." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE,
			//				UserType.HR,
			//				UserType.IT
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 2,
			//			Name = "Welcome Email",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "HR will send welcome email to new hire." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "HR will send welcome email to new hire." },
			//				new TaskDescription { Viewer = UserType.HR, Instructions = "Send welcome email to new hire." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE,
			//				UserType.HR
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 3,
			//			Name = "Mandatory Trainings",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Sign new hire up for applicable trainings." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Obtain list of mandatory trainings from your manager.  Complete each training." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 4,
			//			Name = "Background Verification",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Verify with HR that employee's background verification is complete." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Verify with your manager that your background verification is completed." },
			//				new TaskDescription { Viewer = UserType.HR, Instructions = "Send employee information to xxx for background verification.  Notify Manager when complete." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE,
			//				UserType.HR
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 5,
			//			Name = "PM Sync-Up",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Meet with new-hire." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Meet with manager." },
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 6,
			//			Name = "Seat & Asset Allocation",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Submit form xxx to tech for seat and asset allocation." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Work with manager to get a seat and computer." },
			//				new TaskDescription { Viewer = UserType.IT, Instructions = "Install computer at new employee's assigned seating area." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE,
			//				UserType.IT
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 7,
			//			Name = "Mailbox Activation",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Submit form xxx to tech for Mailbox Activation." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Work with manager to get your mailbox setup." },
			//				new TaskDescription { Viewer = UserType.IT, Instructions = "Setup new employee's mailbox." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE,
			//				UserType.IT
			//			}
			//		},
			//		new WorkflowTask
			//		{
			//			Id = 8,
			//			Name = "About Me Email",
			//			Descriptions = new List<TaskDescription>
			//			{
			//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Work with new hire on what content should be in about me email they send out." },
			//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Send email to team with information about yourself to introduce yourself to the team." }
			//			},
			//			Viewers = new List<UserType>
			//			{
			//				UserType.MANAGER,
			//				UserType.EMPLOYEE
			//			}
			//		},
			//	}
			//};
		}
	}

	//public class WorkflowPost201ResponseExample : IExamplesProvider
	//{
	//	public object GetExamples()
	//	{
	//return new Workflow
	//{
	//	Id = "CloudOffshoreExternal",
	//	Name = "CloudOffshoreExternal",
	//	Description = "Onboarding tasks for offshore external Cloud employees.",
	//	Tasks = new List<WorkflowTask>
	//	{
	//		new WorkflowTask
	//		{
	//			Id = 1,
	//			Name = "Report to RMG",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Fill out request form xxxx and send to HR." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Ask manager to send ID creation request to HR." },
	//				new TaskDescription { Viewer = UserType.HR, Instructions = "Fill out request form xxxx and send to tech dept for id creation.  Upon response, notify manager of new employee id." },
	//				new TaskDescription { Viewer = UserType.IT, Instructions = "generate new id and return id information to HR." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE,
	//				UserType.HR,
	//				UserType.IT
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 2,
	//			Name = "Welcome Email",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "HR will send welcome email to new hire." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "HR will send welcome email to new hire." },
	//				new TaskDescription { Viewer = UserType.HR, Instructions = "Send welcome email to new hire." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE,
	//				UserType.HR
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 3,
	//			Name = "Mandatory Trainings",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Sign new hire up for applicable trainings." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Obtain list of mandatory trainings from your manager.  Complete each training." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 4,
	//			Name = "Background Verification",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Verify with HR that employee's background verification is complete." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Verify with your manager that your background verification is completed." },
	//				new TaskDescription { Viewer = UserType.HR, Instructions = "Send employee information to xxx for background verification.  Notify Manager when complete." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE,
	//				UserType.HR
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 5,
	//			Name = "PM Sync-Up",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Meet with new-hire." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Meet with manager." },
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 6,
	//			Name = "Seat & Asset Allocation",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Submit form xxx to tech for seat and asset allocation." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Work with manager to get a seat and computer." },
	//				new TaskDescription { Viewer = UserType.IT, Instructions = "Install computer at new employee's assigned seating area." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE,
	//				UserType.IT
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 7,
	//			Name = "Mailbox Activation",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Submit form xxx to tech for Mailbox Activation." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Work with manager to get your mailbox setup." },
	//				new TaskDescription { Viewer = UserType.IT, Instructions = "Setup new employee's mailbox." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE,
	//				UserType.IT
	//			}
	//		},
	//		new WorkflowTask
	//		{
	//			Id = 8,
	//			Name = "About Me Email",
	//			Descriptions = new List<TaskDescription>
	//			{
	//				new TaskDescription { Viewer = UserType.MANAGER, Instructions = "Work with new hire on what content should be in about me email they send out." },
	//				new TaskDescription { Viewer = UserType.EMPLOYEE, Instructions = "Send email to team with information about yourself to introduce yourself to the team." }
	//			},
	//			Viewers = new List<UserType>
	//			{
	//				UserType.MANAGER,
	//				UserType.EMPLOYEE
	//			}
	//		},
	//	}
	//};
}