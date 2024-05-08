using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MyPokedexBusiness;
using MyPokeDexWeb.Model;
using System.Security.Claims;

namespace MyPokeDexWeb.Pages.Account
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class DeleteUsersModel : PageModel
    {
        [BindProperty]
        public Person profile { get; set; } = new Person();
        public List<Person> PersonItems { get; set; } = new List<Person>();
        public void OnGet()
        {
            PopulateAllProfiles();


        }
        public class Person
        {
            public int PersonID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int RoleID { get; set; }
            public string Phone { get; set; }
            public DateTime LastLoginTime { get; set; }
            // Other properties...
        }

        private void PopulateAllProfiles()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT  p.PersonID, p.FirstName, p.LastName, p.Email, p.RoleID, p.Phone,
                                   p.LastLoginTime
                           FROM Person p";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new Person();
                        item.PersonID = reader.GetInt32(0);
                        item.FirstName = reader.GetString(1);
                        item.LastName = reader.GetString(2);
                        item.Email = reader.GetString(3);
                        item.RoleID = reader.GetInt32(4);
                        item.Phone = reader.GetString(5);
                       

                        PersonItems.Add(item);
                    }
                }
            }
        }
    }
}
