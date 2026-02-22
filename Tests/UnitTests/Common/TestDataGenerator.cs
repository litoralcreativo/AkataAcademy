using System;
using System.Linq;

namespace AkataAcademy.UnitTests.Common
{
	public static class TestDataGenerator
	{
		private static readonly Random random = new Random(42); // Semilla fija para reproducibilidad

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static int RandomDuration(int min = 10, int max = 180)
		{
			return random.Next(min, max);
		}
	}
}
