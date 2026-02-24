namespace AkataAcademy.Domain.Common
{
	public static class ErrorCodes
	{
		public static class Course
		{
			public const string NotFound = "Course.NotFound";
			public const string Publishing = "Course.Publishing";
			public const string Creation = "Course.Creation";
			public const string ModuleManagment = "Course.ModuleManagment";

		}

		public static class Certificate
		{
			public const string Creation = "Certificate.Creation";
			public const string IssueDenied = "Certificate.IssueDenied";
			public const string NotFound = "Certificate.NotFound";
		}

		public static class Eligibility
		{
			public const string Creation = "Eligibility.Creation";
			public const string StateTransition = "Eligibility.StateTransition";
			public const string NotFound = "Eligibility.NotFound";
		}
		public static class Student
		{
			public const string Creation = "Student.Creation";
			public const string StatusChange = "Student.StatusChange";
			public const string NotFound = "Student.NotFound";
		}

		public static class General
		{
			public const string Null = "General.Null";
			public const string Empty = "General.Empty";
			public const string Conflict = "General.Conflict";
		}
	}
}