using BCrypt.Net;
namespace MyPokedexBusiness
{
	

	public class SecurityHelper
	{
		public static string GeneratePasswordHash(string password)
		{
			return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);

		}

		public static bool VerifyPassword( string password, string passwordHash)
		{
			return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
		}
		public static string GetDBConnectionString()
		{
			string connString = "Server=(localdb)\\MSSQLLocalDB;Database=MyPokedex;Trusted_Connection=true;";
			return connString;
		}

	}
}
