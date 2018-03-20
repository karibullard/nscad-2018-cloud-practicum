using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Models
{
	/// <summary>
	/// Type of user.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserType
	{
		/// <summary>
		/// Indicates a user is a manager.
		/// </summary>
		manager,

		/// <summary>
		/// Indicates a user is an new-hire employee.
		/// </summary>
		employee,

		/// <summary>
		/// Indicates a user is an HR department employee.
		/// </summary>
		hr,

		/// <summary>
		/// Indicates a user is an IT department employee.
		/// </summary>
		it
	}
}