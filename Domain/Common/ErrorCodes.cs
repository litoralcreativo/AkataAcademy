namespace AkataAcademy.Domain.Common
{
	public static class ErrorCodes
	{
		public static class Course
		{
			public const string NotFound = "Course.NotFound";
		}

		public static class Certificate
		{
			public const string IssueDenied = "Certificate.IssueDenied";
			public const string NotFound = "Certificate.NotFound";
		}

		public static class General
		{
			public const string Null = "General.Null";
		}
	}
}